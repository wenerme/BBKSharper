using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

using ThemeSim;
using ThemeSim.Scripts;
using ThemeSim.ThemeSettings;
using ThemeSim.ThemeElements;
using log4net;

public static class MainScript
{
	static MainScreenScript MainScreen = null;
	static ScreenScript BatterScreen = null;
	static ScreenScript USBScreen = null;
	public static void OnLoad(Form mainForm, ThemeSimulator sim, ILog logger)
	{
		logger.Info("�ɹ����ؽű�.");
		MainScreen = new MainScreenScript();
		BatterScreen = new BatteryScreenScript();
		USBScreen = new EscapeToLastScreenScript();
		{
			ScreenControl screen;
			screen = sim.GetObjectByRefer("MainScreen") as ScreenControl;
			MainScreen.BindControl(screen);

			screen = sim.GetObjectByRefer("BatteryScreen") as ScreenControl;
			BatterScreen.BindControl(screen);

			USBScreen.BindControl(sim.GetObjectByRefer("USBScreen") as ScreenControl);
		}

	}
}

public class MainScreenScript : ScreenScript
{
	ImageControl icon;
	ImageControl iconBar;
	ImageControl battery;
	ImageControl bottomMenu;
	int currentSelectedBottomMenu = 0;
	Rectangle bottomMenuArea = new Rectangle(24, 276, 195, 46);
	string[] menuList = { "��ʵ�", "������", "����˵", "ѧ����", "������", "����Ӱ", "����Ϸ", "�ҵ����", "�ҵ����", "����" };
	int menuId = 0;
	IDisposable batteryInterval;

	EventHandler nextHandler;
	EventHandler prevHandler;

	public MainScreenScript()
	{
		nextHandler = (sender, e) => { NextMenu(); };
		prevHandler = (sender, e) => { PrevMenu(); };
	}

	protected override void OnDisplay()
	{
		icon = Simulator.GetObjectByRefer("imgͼ��") as ImageControl;
		iconBar = Simulator.GetObjectByRefer("imgͼ��Bar") as ImageControl;
		battery = Simulator.GetObjectByRefer("img���") as ImageControl;
		bottomMenu = Simulator.GetObjectByRefer("img���˵�") as ImageControl;
		// ��ʼʱmenu����ʾ
		bottomMenu.Visible = false;
		bottomMenu.AutoSize = false;
		bottomMenu.Width = (bottomMenuArea.Width / 5);

		iconBar.MouseClick += DoMenuBarClick;
		//
		batteryInterval = U5.SetInterval(() =>
		{
			battery.Invoke(new Action(() => { battery.OffsetTop = (battery.OffsetTop - 10) % -40; }));
		}, 1000);
		// �����Ҽ�ͷ����
		{
			Control ctr = Simulator.GetObjectByRefer("img���ͷ") as Control;
			ctr.Click += prevHandler;
			ctr = Simulator.GetObjectByRefer("img�Ҽ�ͷ") as Control;
			ctr.Click += nextHandler;
		}
		// ����ʱ�������
		{
			Control ctr = Simulator.GetObjectByRefer("txtʱ��") as Control;
			ctr.Text = DateTime.Now.ToString("hh:mm");

			ctr = Simulator.GetObjectByRefer("txt����") as Control;
			ctr.Text = DateTime.Now.ToString("yy/MM/dd");
		}
		
	}
	protected override void OnHide()
	{
		batteryInterval.Dispose();
		iconBar.MouseClick -= DoMenuBarClick;
		//
		{
			Control ctr = Simulator.GetObjectByRefer("img���ͷ") as Control;
			ctr.Click -= prevHandler;
			ctr = Simulator.GetObjectByRefer("img�Ҽ�ͷ") as Control;
			ctr.Click -= nextHandler;
		}
	}

	protected override void OnKeyUp(object sender, KeyEventArgs e)
	{
		switch(e.KeyCode)
		{
			case Keys.Left:
				PrevMenu();
				break;
			case Keys.Right:
				NextMenu();
				break;
		}
	}


	#region �ײ��˵�
	// ����ѡ�����˵�����ʾ
	protected override void OnMouseMove(object sender, MouseEventArgs e)
	{

		int x = e.X;
		int y = e.Y;

		if(false == bottomMenuArea.Contains(x, y))
		{
			HideSelectedMenu();
			return;
		}

		// �����ǵڼ����˵�
		currentSelectedBottomMenu = (x - bottomMenuArea.Left) / (bottomMenuArea.Width / 5);

		ShowSelectedMenu();
	}

	public void ShowSelectedMenu(int i = int.MaxValue)
	{
		if(i == int.MaxValue)
			i = currentSelectedBottomMenu;
		else
			currentSelectedBottomMenu = i;
		// У��
		currentSelectedBottomMenu = currentSelectedBottomMenu < 0 ? 4 : currentSelectedBottomMenu;
		currentSelectedBottomMenu = currentSelectedBottomMenu > 4 ? 0 : currentSelectedBottomMenu;
		//
		i = currentSelectedBottomMenu;
		bottomMenu.OffsetLeft = -i * (bottomMenuArea.Width / 5);
		bottomMenu.Left = bottomMenuArea.Left + i * (bottomMenuArea.Width / 5);
		//
		bottomMenu.Visible = true;
	}

	public void HideSelectedMenu()
	{
		bottomMenu.Visible = false;
	}
	#endregion

	#region  �м�ͼ��

	void DoMenuBarClick(object sender, MouseEventArgs e)
	{
		int[] menuBarSperate = new int[] { 0 , 44, 88, 152, 196, 240};

		int i = 0;
		for(; i < menuBarSperate.Length - 1; i++)
		{
			if(e.X > menuBarSperate[i] && e.X < menuBarSperate[i + 1])
				break;
		}
		ShowMenu(menuId + i - 2);
	}

	public void NextMenu()
	{
		ShowMenu(menuId + 1);
	}

	public void PrevMenu()
	{
		ShowMenu(menuId - 1);
	}

	public void ShowMenu(int id = int.MaxValue)
	{
		if(id == int.MaxValue)
			id = menuId;
		else
			menuId = id;
		// У��
		menuId = menuId < 0 ? menuList.Length - 1 : menuId;
		menuId = menuId >= menuList.Length ? 0 : menuId;
		//
		id = menuId;
		string menuName = menuList[id];
		icon.Image = Simulator.GetObjectByRefer("IMG" + menuName) as Image;
		iconBar.Image = Simulator.GetObjectByRefer("IMG" + menuName + "Bar") as Image;
	}
	#endregion

}

public class EscapeToLastScreenScript : ScreenScript
{
	protected override void OnKeyUp(object sender, KeyEventArgs e)
	{
		logger.Info("Excape KeyUp");
		switch(e.KeyCode)
		{
			case Keys.Escape:
				System.Diagnostics.Debugger.Break();
				Simulator.Screen.ShowLastScreen();
				break;
		}
	}
}

public class BatteryScreenScript : ScreenScript
{
	IDisposable batteryInterval;
	
	const int batteryImageMaxId = 4;
	const int batteryImageMinId = 1;
	int batteryImageId = batteryImageMinId;
	ImageControl battery;

	protected override void OnDisplay()
	{
		battery = Simulator.GetObjectByRefer("img����") as ImageControl;
		batteryInterval = U5.SetInterval(() =>
		{
			batteryImageId++;
			batteryImageId = batteryImageId > batteryImageMaxId ? batteryImageMinId : batteryImageId;

			battery.Invoke(new Action(() =>
			{
				battery.Image = Simulator.GetObjectByRefer("IMG����" + batteryImageId) as Image;
			}));
			
		}, 1000);
	}

	protected override void OnHide()
	{
		batteryInterval.Dispose();
	}
	protected override void OnKeyUp(object sender, KeyEventArgs e)
	{
		logger.Info("Bettery KeyUp");
		switch(e.KeyCode)
		{
			case Keys.Escape:
				Simulator.Screen.ShowLastScreen();
				break;
		}
	}
}