   M S B A M L   `   `   `  ????F  BWorldSkills, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorldSkills.PageWorldSkills.Page  5   	   =#clr-namespace:WorldSkills.ViewModelWorldSkills.ViewModel  5   	   ,    &WorldSkills.Page.AdministratorMainMenu   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WorldSkills.Page     5   	   9	viewmodel#clr-namespace:WorldSkills.ViewModel     5   	     ? WindowStyle$
  None=?5   :    ? WindowStartupLocation$ CenterScreen=?5   	    ? Title$' !BELAVIA Airline Automation System??5   	   $	??450??63   $	??800??6C   $	??450??6R   $	??800??6b   .??5      ? $	??450??6   $	??800??6   $	??450??6*   $	??800??6:   $	??345??6I   $	??715??6V    ? ColumnDefinitions 5   
   ?? 5      $	??17* ?6   ?? 5      $	??15* ?6   .+?5   
   c? $??29??6   $	??Top=?6   $??2??66   .@?5      ?? -   5        $??Add user??5      6)   ?? 5      -   5        $??Manage Flight Schedules??5      67   ?? 5      -   5        $??Get Information about day??5      69   ?? 5      -   5        $
??Exit??5      6%   5      ?? 5   
   $??Office: ??6   $??20,45,360,381q?6%   ?? 5   
   -   5        $??OfficesComboBox??5      $??80,47,215,378q?6+    ?? DisplayMemberPath$ Title??5   	   ??5   B   ?? *	AllOffice+ ?? Text 5   #   ?? *SelectedOffice+?? 5   
   -   5        
???????$??Change Role??5   $   $??42,387,295,43q?6:   $??20??6R   $??88??6^   6i   ?? 5   
   
???????$??Enable/Disable Login??6$   $??190,387,110,43q?6D   $??20??6\   $	??125??6h   ??6t   ?? *EnableDisableLogin+??6?   ?? *SelectedUser+?? 5   
   $??80,81,80,81q?6   $??0??6(   $??2??6?   ??68   ?? *
AllUsers+ ?? SelectedItem 6Z   ?? *SelectedUser+ ?? View 5      ?? 5      .P?5      ??  ?? DisplayMemberBinding 5       ?? 	 ?? Path$	 	FirstName?.??6M   
First Name5!      ?? 5"       5#      ?? $	 LastName?.??6L   	Last Name5$      ?? 5%       5&      ?? $		 Age?.??6G   Age5'      ?? 5(       5)      ?? $	 
RoleIDText?.??6N   
UserRole5*      ?? 5+       5,      ?? $	 Email?.??6I   Email Address5-      ?? 5.       5/      ?? $	 OfficeIDText?.??6P   Office50      51      ??53      ?? 54      
 ?? 
TargetType"
 ??6    ?? Triggers 55      ?? 56       ?? Binding 6&   ?? $	 RoleID? ?? Value 16F   .a?57      ??  ?? Property ? ?? ?? Value
 ? ? ?6;   58      ?? 59       6&   ?? $	 Active?	 false6F   .a?5:      ??  ? ??
 ?  ??6;   5;      5=      5?      5@      5A      