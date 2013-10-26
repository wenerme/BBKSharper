-- ���������ռ�
-- �ڸð汾��, ���� LuaInterface ��ʹ�õ�Ŷ Assemblly.Load �����ص�
-- ��Ҫʹ��ȫ��,������ػ�ʧ��
luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
luanet.load_assembly "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

Point = luanet.import_type("System.Drawing.Point")
MessageBox = luanet.import_type "System.Windows.Forms.MessageBox"
Keys = luanet.import_type "System.Windows.Forms.Keys"


luanet.load_assembly("ThemeSim");
-- luanet.load_assembly("ThemeSim.ThemeElements");

-- �����
U5 = luanet.import_type("ThemeSim.U5");
CtrExt = luanet.import_type("ThemeSim.ThemeElements.ControlExtension");
-- �ڽű�ִ���ڼ�,������ȫ�ֱ���
-- logger, mainForm �� themeSim �����Գ�����п��ƺ͵���
-- �����Ҫ�󶨰���, ����mainForm�ϼ���




-- �ű����ݲ���
logger:Info("�ɹ����ؽű�.");


function OnLoad()
logger:Info("���� OnLoad");

screen = themeSim:GetObjectByRefer("MainScreen")
screen.StateChanged:Add(MainScreenState);

screen = themeSim:GetObjectByRefer("BetteryScreen")
screen.StateChanged:Add(BetteryScreenState);

end
-- BetteryScreen ������
do
local interval = nil;
local betteryScreenKeyUpHandler = nil;
	function BetteryScreenState(sender, state)
		if state then
			logger:Info("��ʾ ��������");
			batteryId = 1;
			interval = U5.SetInterval(function() 
				batteryId = (batteryId + 1) % 5;
				if batteryId == 0 then
					batteryId = 1;
				end
				themeSim:GetObjectByRefer("img����").Image = 
					themeSim:GetObjectByRefer("IMG����"..batteryId);
			end, 1000);
			--
			betteryScreenKeyUpHandler = mainForm.KeyUp:Add(BetteryScreenKeyUp);
		else
			interval:Dispose();
			mainForm.KeyUp:Remove(betteryScreenKeyUpHandler);
		end--if
	end
	
	function BetteryScreenKeyUp(sender, e)
		logger:Info(e.KeyCode);
		if e.KeyCode == Keys.Escape then
			themeSim.Screen:ShowLastScreen();
		end
	end
end

-- MainScreen ������
do
	local initialized = false;
	local menuList = {'��ʵ�','������','����˵','ѧ����','������','����Ӱ','����Ϸ','�ҵ����','�ҵ����','����'};
	local currentMenuId = 1;
	local battery = nil;
	local batteryInterval = nil;
	local icon = nil;
	local iconBar = nil;
	-- ��ʼ
	function MainScreenInit()
		-- ֻ��ʼһ��
		if initialized then
			logger:Info("No init");
			return;
		else
			initialized = true;
		end
		--
		icon = themeSim:GetObjectByRefer("imgͼ��");
		iconBar = themeSim:GetObjectByRefer("imgͼ��Bar");
		
		-- ���Ҽ�ͷѡ��˵�
		local ctr = themeSim:GetObjectByRefer("img���ͷ");
		ctr.Click:Add(PrevMenu);
		ctr = themeSim:GetObjectByRefer("img�Ҽ�ͷ");
		ctr.Click:Add(NextMenu);
			
	end -- MainScreenInit

	
	-- ����MainScreen״̬
	function MainScreenState(sender, state)

		if state then
			logger:Info("��ʾ ����Ļ");
			MainScreenInit();
			
			-- ѭ����ʾ���״̬
			battery = themeSim:GetObjectByRefer("img���");
			batteryInterval = U5.SetInterval(function() 
				CtrExt.SetTop(battery,(battery.Top - 10) % -40);
				CtrExt.SetHeight(battery,10 - battery.Top);
			end, 1000);
			--
			mainScreenKeyUpHandler = mainForm.KeyUp:Add(MainScreenKeyUp);
		else
			logger:Info("�ر� ����Ļ");
			batteryInterval:Dispose();
			mainForm.KeyUp:Remove(mainScreenKeyUpHandler);
		end

	end -- MainScreenState

	function MainScreenKeyUp(sender, e)
		if e.KeyCode == Keys.Left then
			PrevMenu()
		elseif e.KeyCode == Keys.Right then
			NextMenu()
		end
	end
	
	function NextMenu()
		currentMenuId = currentMenuId + 1;
		if currentMenuId > #menuList then
			currentMenuId = 1;
		end
		logger:Info("NextMenu");
		ShowMenu();
	end
	
	function PrevMenu()
		currentMenuId = currentMenuId - 1;
		if currentMenuId == 0 then
			currentMenuId = #menuList;
		end
		logger:Info("PrevMenu");
		ShowMenu();
	end
	
	function ShowMenu(id)
		if id == nil then
			id = currentMenuId;
		end
		logger:Info("ShowMenu");
		if id < 0 or id > #menuList then
			logger:Info("����� ID ��");
			return;
		end
		menuName = menuList[id];
		logger:Info(string.format("ID: %s ,name=%s", id, menuName));
		logger:Info("ShowIcon");
		icon.Image = themeSim:GetObjectByRefer("IMG"..menuName);
		logger:Info("ShowIcon 2");
		iconBar.Image = themeSim:GetObjectByRefer("IMG"..menuName..'Bar');
		logger:Info("ShowIcon Down");
	end
	
end
