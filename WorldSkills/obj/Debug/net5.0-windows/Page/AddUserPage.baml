   M S B A M L   `   `   `  ????F  BWorldSkills, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorldSkills.PageWorldSkills.Page  5   	   =#clr-namespace:WorldSkills.ViewModelWorldSkills.ViewModel  5   	   "    WorldSkills.Page.AddUserPage   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WorldSkills.Page     5   	   9	viewmodel#clr-namespace:WorldSkills.ViewModel     5   	     ? WindowStyle$  
ToolWindow=?5   0    ? WindowStartupLocation$ CenterScreen=?5   	    ? Title$ Add User??5
   	   $	??373??6   $	??500??6*   $	??373??69   $	??500??6I    ? 	Resources 5      .   (WorldSkills.ViewModel.UserMultyConverter   	Converter??%   &	          5   
   6'   5      .??5      ? $	??373??6   $	??500??6   .+?5   
   ?? $??Email Address:??6   $??16(?6)   $??19,34,368,307q?67   ?? 5   
   $??First name:??6   $??16(?6&   $??19,71,368,267q?64   ?? 5   
   -   5         ??Name$ EmailTextBox??5      $??132,39,44,313q?6(   ?? 5   
   -   5        $ FirstNameTextBox??5      $??132,78,44,274q?6,   ?? 5   
   $??
Last name:??6   $??16(?6&   $??19,105,368,236q?64   ?? 5   
   $??Office:??6   $??16(?6"   $??19,142,368,196q?60   ?? 5   
   -   5        $ LastNameTextBox??5      $??132,110,44,242q?6+   ?? 5   
   -   5         ??Name$ ComboBoxOffice??5      $??132,147,44,204q?6+    ?? DisplayMemberPath$ Title??5   	   ??5   C   ?? *	AllOffice+5   	   ?? 5   
   $??
Birthdate:??6   $??16(?6%   $??19,182,368,156q?63   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ BirthdatePicker??5      $
??Left=?6.   $??132,186,0,0q?6I   $	??Top=?6^   $	??324??6v      FirstDayOfWeek$ Monday=?6?   ?? 5   
   $??	Password:??6   $??16(?6$   $??19,218,368,120q?62   ?? 5   
   -   5        $ PasswordTextBox??5      $??132,227,44,125q?6+   ?? 5   
   $
??Save??6   
???????$??122,304,290,49q?63   $??20??6L   $??88??6X   ??6c   ?? *
AddCommand+??5      `? 5      	 `? 	Converter#	 [  6   .1?5      ?? 
 ?? ElementName$
 EmailTextBox??6    ?? Path$
 Text?69   ?? 5       $
 FirstNameTextBox??6   $
 Text?6=   ?? 5!      $
 LastNameTextBox??6   $
 Text?6<   ?? 5"      $
 ComboBoxOffice??6   $
 Text?6;   ?? 5#      $
 BirthdatePicker??6   $
 Text?6<   ?? 5$      $
 PasswordTextBox??6   $
 Text?6<   5%      5'      ?? 5(   
   -   5        
???????$??Cancel??5(   $   $??270,304,105,49q?66   $??20??6N   $	??125??6Z   6f   5)      5*      