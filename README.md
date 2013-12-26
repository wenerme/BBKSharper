<!-- title: 步步高学习机图片格式浏览/修改工具(DLX,RLB,LIB) 和 BBKSharper -->
<!-- category: Tools -->
<!-- tag: C# -->
<!-- date: 2013/8/27 -->
<!-- state: published -->

这个解决方案包括了三个项目

* [BBKSharper](#BBKSharper)
* [ImageView](#ImageView)
* [ThemeSim](#ThemeSim)

BBKSharper
===

支持步步高设备文件的.net库,使用C#写成.当前仅支持 RLB/DLX/LIB 的读写.

ImageView
==========

支持对 RLB/DLX/LIB 的编辑.绑定了较多快捷键

<!--more-->


特性
----

* 拖动添加图片,可以添加DLX,LIB,RLB或JPG,PNG,GIF,BMP等
* 可直接导出文件
* 可直接导出图片
* 可替换,或移动图片
* 简单易用的操作

快捷键
-----

* 左键: 上一张
* 右键: 下一张
* O: 打开文件
* DELETE: 移除当前显示的图片

ChangeLog
----------

### ChangeLog: 2013.9.17

 * 使用 Ookii.Dialog 作为选择路径的对话框
 + 更改拖动进图片后,显示拖动进的图片
 + 增加右键清除所有
 + 增加添加时自动清除选项,并且默认为 选中
 + 拖入文件时,会记住最后一个文件名,并且保存时作为默认文件夹名

 
ThemeSim
========

模拟 步步高 主题.使用XML进行配置,使用 C# 进行脚本编写.

该软件尚未完成.

截图
====

![ImageView screenschot](https://raw.github.com/WenerLove/BBKSharper/master/ImageView_ScreenShot.png "ImageView")


![ThemeSim screenschot](https://raw.github.com/WenerLove/BBKSharper/master/ThemeSim_ScreenShot.png "ThemeSim")

 
