   M S B A M L   `   `   `  ????F  BWorldSkills, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorldSkills.PageWorldSkills.Page  5   	   =#clr-namespace:WorldSkills.ViewModelWorldSkills.ViewModel  66   *    $WorldSkills.Page.GetInformationAdmin   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:WorldSkills.Page     5   	   9	viewmodel#clr-namespace:WorldSkills.ViewModel     66     ? Title$  GetInformationAdmin??5   	   $	??308??6%   $	??678??62   .??5	      ? .+?5
   
   ?? $+??%Введите дату полета:??6   $
??Left=?6/   $??	10,10,0,0q?6J   $	??Top=?6]   ?? 5   
   -   5         ??Name$ FlightIDString??5      $
??Left=?6*   $??	16,41,0,0q?6E    ?? TextWrapping$
 Wrap=?6X   $	??Top=?6l   $	??151??6?   $??22??6?   ?? 5   
   $??Ввести??6   
???????$
??Left=?65   $??	16,72,0,0q?6P   $	??Top=?6c   ??6{   ?? *GetInformationAboutDay+??6?   ??  ?? ElementName$ FlightIDString?? ?? Path$
 Text??? 5   
   $??16,119,10,28q?6   ??6)   ?? *AircraftsDateAmenities+ ?? View 5      ?? 5      .P?5      ??  ?? DisplayMemberBinding 5      ?? $ NameAmemities?.??6Q   Name Amemities5      ?? 5       5      ?? $ CountAmenities?.??6R   Count Amenities5      5      5      5      5      