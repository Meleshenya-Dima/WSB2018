   M S B A M L   `   `   `  ????F  BWorldSkills, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorldSkills.PageWorldSkills.Page  5   	   =#clr-namespace:WorldSkills.ViewModelWorldSkills.ViewModel  66   *    $WorldSkills.Page.DefaultUserMainMenu   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WorldSkills.Page     5   	   9	viewmodel#clr-namespace:WorldSkills.ViewModel     66     ? WindowStyle$
  None=?5   8    ? WindowStartupLocation$ CenterScreen=?5   	    ? Title$' !BELAVIA Airline Automation System??5	   	   $	??450??63   $	??800??6C   $	??450??6R   $	??800??6b   .??5
      ? $	??450??6   $	??800??6   $	??450??6*   $	??800??6:   .+?5   
   c? $??25??6   $	??Top=?6   .@?5      ?? -   5        $??Search For Flights??5      62   ?? 5      -   5        $??Additional services??5      63   ?? 5      $
??Exit??6   ??6%   ?? *UpdateLogoutCommand+??6M   ??  ?? Path$ NumberOfCrashes?5      ?? 5   
   $??10,30,445,386q?6<   ??6   ?? $ IntroductionText?6<   ?? 5   
   $??Time spent on system:??6   $??320,66,350,354q?60   ?? 5   
   $??Number of crashes:??6   $??570,66,110,354q?6-   ?? 5   
   $??80,101,80,76q?6   $??0??6)   ??69   ?? *
Tracking+ ?? View 5      ?? 5      .P?5      ??  ?? DisplayMemberBinding 5      ?? $
 Date?.??6H   Date5      ?? 5       5      ?? $ 	LoginTime?.??6M   
Login time5      ?? 5       5      ?? $ 
LogoutTime?.??6N   Logout time5      ?? 5       5       ?? $ TimeSpentOnSystem?.??6U   Time spent on system5!      ?? 5"       5#      ?? $ UnsuccessfulLogoutReason?.??6\   Unsuccessful logout reason5$      5%      ??5'      ?? 5(       ?? 
TargetType" ??6    ?? Triggers 5)      ?? 5*       ?? Binding 6&   ?? $ IsException?	 ?? Value	 True6K   .a?5+      ?? 
 ?? Property
 ? ?? ?? Value
 ?  ??6;   5,      5.      50      ?? 51   
   $
??Left=?63   $??
455,68,0,0q?6N   $	??Top=?6b   $??28??6z   $??65??6?   ??6   ?? $ 	TimeSpent?6?   ?? 52   
   $
??Left=?6:   $??
690,66,0,0q?6U   $	??Top=?6i   $??28??6?   $??35??6?   ??6   ?? $ NumberOfCrashes?6?   53      54      