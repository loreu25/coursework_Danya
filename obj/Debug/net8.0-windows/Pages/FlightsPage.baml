   M S B A M L   `   `   `  ����  TicketBookingSystem+    %TicketBookingSystem.Pages.FlightsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  
Рейсы��5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   c� $��0��6   .@�5      �� -   5         ��Name$ btnAdd��5      $��Добавить��6%   $��0,0,5,0q�6M   �� 5      -   5        $ 	btnDelete��5      $��Удалить��6(   6:   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dgFlights��5      $��1��6&   $��0,10,0,0q�63      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��6/      SelectionMode$ Single=�5         SelectionUnit$ FullRow=�6*      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Номер рейса��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6:   �� *FlightNumber+  5      $	 Тип��6%   
 62   �� *
FlightType+  5      $	 Откуда��6%   
 65   �� *DepartureCity+  5      $	 Куда��6%   
 63   �� *ArrivalCity+  5      $	 Отправление��6%   
 6:   �� *DepartureTime+ �� StringFormat$ {0:dd.MM.yyyy HH:mm}��  5      $	 Прибытие��6%   
 67   �� *ArrivalTime+$ {0:dd.MM.yyyy HH:mm}��  5      $	 Базовая цена��6%   
 6;   �� *	BasePrice+$ 
{0:N2} ₽��  5      $!	 Свободных мест��6%   
 6=   �� *AvailableSeats+4  .System.Windows.Controls.DataGridTemplateColumn  5         Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6)      CellTemplate 5       �� 5!      .R�5"      �� -   5        $ ��Редактировать��5"   %   6=   5#      5%      5'      5(      5)      