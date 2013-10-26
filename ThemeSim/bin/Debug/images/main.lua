-- 导入命名空间
-- 在该版本里, 由于 LuaInterface 是使用的哦 Assemblly.Load 来加载的
-- 需要使用全名,否则加载会失败
luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
luanet.load_assembly "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

Point = luanet.import_type("System.Drawing.Point")
MessageBox = luanet.import_type "System.Windows.Forms.MessageBox"
Keys = luanet.import_type "System.Windows.Forms.Keys"


luanet.load_assembly("ThemeSim");
-- luanet.load_assembly("ThemeSim.ThemeElements");

-- 便捷类
U5 = luanet.import_type("ThemeSim.U5");
CtrExt = luanet.import_type("ThemeSim.ThemeElements.ControlExtension");
-- 在脚本执行期间,有两个全局变量
-- logger, mainForm 和 themeSim 用来对程序进行控制和调试
-- 如果需要绑定按键, 绑定在mainForm上即可




-- 脚本内容部分
logger:Info("成功加载脚本.");


function OnLoad()
logger:Info("触发 OnLoad");

screen = themeSim:GetObjectByRefer("MainScreen")
screen.StateChanged:Add(MainScreenState);

screen = themeSim:GetObjectByRefer("BetteryScreen")
screen.StateChanged:Add(BetteryScreenState);

end
-- BetteryScreen 处理部分
do
local interval = nil;
local betteryScreenKeyUpHandler = nil;
	function BetteryScreenState(sender, state)
		if state then
			logger:Info("显示 电量界面");
			batteryId = 1;
			interval = U5.SetInterval(function() 
				batteryId = (batteryId + 1) % 5;
				if batteryId == 0 then
					batteryId = 1;
				end
				themeSim:GetObjectByRefer("img电量").Image = 
					themeSim:GetObjectByRefer("IMG电量"..batteryId);
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

-- MainScreen 处理部分
do
	local initialized = false;
	local menuList = {'查词典','背单词','练听说','学课文','听音乐','看电影','玩游戏','我的相机','我的相册','翻译'};
	local currentMenuId = 1;
	local battery = nil;
	local batteryInterval = nil;
	local icon = nil;
	local iconBar = nil;
	-- 初始
	function MainScreenInit()
		-- 只初始一次
		if initialized then
			logger:Info("No init");
			return;
		else
			initialized = true;
		end
		--
		icon = themeSim:GetObjectByRefer("img图标");
		iconBar = themeSim:GetObjectByRefer("img图标Bar");
		
		-- 左右箭头选择菜单
		local ctr = themeSim:GetObjectByRefer("img左箭头");
		ctr.Click:Add(PrevMenu);
		ctr = themeSim:GetObjectByRefer("img右箭头");
		ctr.Click:Add(NextMenu);
			
	end -- MainScreenInit

	
	-- 监听MainScreen状态
	function MainScreenState(sender, state)

		if state then
			logger:Info("显示 主屏幕");
			MainScreenInit();
			
			-- 循环显示电池状态
			battery = themeSim:GetObjectByRefer("img电池");
			batteryInterval = U5.SetInterval(function() 
				CtrExt.SetTop(battery,(battery.Top - 10) % -40);
				CtrExt.SetHeight(battery,10 - battery.Top);
			end, 1000);
			--
			mainScreenKeyUpHandler = mainForm.KeyUp:Add(MainScreenKeyUp);
		else
			logger:Info("关闭 主屏幕");
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
			logger:Info("错误的 ID 号");
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
