   M S B A M L   `   `   `  ????F  BWorldSkills, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorldSkills.PageWorldSkills.Page  5   	   =#clr-namespace:WorldSkills.ViewModelWorldSkills.ViewModel  66   #    WorldSkills.Page.EditRolePage   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WorldSkills.Page     5   	   9	viewmodel#clr-namespace:WorldSkills.ViewModel     66     ? WindowStyle$  
ToolWindow=?5   1    ? WindowStartupLocation$ CenterScreen=?5   	    ? Title$ EditRolePage??5	   	   $	??373??6   $	??500??6.   $	??373??6=   $	??500??6M    ? 	Resources 5
      4   .WorldSkills.ViewModel.UpdateUserMultyConverter   	Converter??%   &	          5   
   6-   5      .??5      ? $	??373??6   $	??500??6   .+?5   
   ?? $??Email Address:??6   $??16(?6*   $??19,34,368,307q?68   ?? 5   
   $??First name:??6   $??16(?6&   $??19,71,368,267q?64   ?? 5   
   -   5         ??Name$ EmailTextBox??5      $??132,39,44,313q?6(   ?? 5   
   -   5        $ FirstNameTextBox??5      $??132,78,44,274q?6,   ?? 5   
   $??
Last name:??6   $??16(?6&   $??19,105,368,236q?64   ?? 5   
   $??Office:??6   $??16(?6"   $??19,142,368,196q?60   ?? 5   
   -   5        $ LastNameTextBox??5      $??132,110,44,242q?6+   ?? 5   
   -   5         ??Name$ OfficeCombobox??5      $??132,144,44,202q?6+    ?? DisplayMemberPath$ Title??6e   ??6C   ?? *	AllOffice+6e   ?? 5   
   $??Role:??6   $??16(?6    $??19,219,368,119q?6.   	? 5   
   -   5         	?Name$ UserRole??5      $
??User??6(   $
??Left=?67   $??282,229,0,0q?6R   $	??Top=?6g   	? 5   
   -   5        $ AdministratorRole??5      $??Administrator??61   $
??Left=?6I   $??282,259,0,0q?6d   $	??Top=?6y   ?? 5   
   
???????$??Apply??6$   $??122,304,290,49q?64   $??20??6M   $??88??6Y   ??6d   ?? *UpdateCommand+??5      `? 5       `? 	Converter# [  6   .1?5      ?? 	 ?? ElementName$	 EmailTextBox??6   
 ?? Path$

 Text?69   ?? 5      $	 FirstNameTextBox??6   $

 Text?6=   ?? 5      $	 LastNameTextBox??6   $

 Text?6<   ?? 5      $	 OfficeCombobox??6   $

 Text?6;   ?? 5       $	 UserRole??6   $
 	IsChecked?65   ?? 5!      $	 AdministratorRole??6   $
 	IsChecked?6>   5"      5$      ?? 5%   
   -   5        
???????$??Cancel??5%   $   $??270,304,105,49q?66   $??20??6N   $	??125??6Z   6f   5&      5'      