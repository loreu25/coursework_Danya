   M S B A M L   `   `   `  ����N  JTicketBookingSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:TicketBookingSystem.ConvertersTicketBookingSystem.Converters  5      ,    &TicketBookingSystem.Pages.BookingsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      C
converters,clr-namespace:TicketBookingSystem.Converters     5        N� Title$  Бронирования��5       � 	Resources 5      @   :TicketBookingSystem.Converters.StatusToVisibilityConverter   StatusToVisibilityConverter  FilterButtonStyle �� 
TargetType �� Property �� Value  SelectedFilterButtonStyle �� BasedOn��%�  &	        &	      &	 �     7    5   
   61   �� 5
   
   " ��6*   .�5      ��  � ��$ 5q�6'   �� 5       � ��$
 10,5q�6(   �� 5       � �� �#F0F0F06+   �� 5       � �� �#CCCCCC6,   5      �� 5   
   " ��62   8   .�5      ��  � �� �#007ACC6+   �� 5       � ��
 �����6+   �� 5       � �� �#0059996,   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ btnAddBooking��5      $)��#Новое бронирование��6(   #��[ 5       $	��150��6<   $
��Left=�6H   �� 5#   
   -   5         ��Name$ filterButtons��5#      $��1��6,   	 �� Orientation$	 
Horizontal=�69   $��	0,10,0,10q�6R   .+�5$      �� -   5        $��Все��5$      

 � Tag$
  ��6#   #��[ 6B   �� 5%      -   5        $"��Подтвержденные��5%      $
 Подтверждено��6.   #��[ 6Y   �� 5&      -   5        $��Отмененные��5&      $
 Отменено��6*   #��[ 6Q   5'      &   System.Windows.Controls.DataGrid  5*   
   -   5          Name$ 
dgBookings��5*      $��2��6'      AutoGenerateColumns$ False��64      
IsReadOnly$
 True��6P      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ Пассажир��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *PassengerName+6Y     5-      $ Контакт��6%   $ * 6[    66   �� *PassengerContact+6[     5.      $ Рейс��6%   $ * 6[    63   �� *Flight.FlightNumber+6[   4  .System.Windows.Controls.DataGridTemplateColumn  5/      $ Маршрут��6)   $ * 6:      CellTemplate 50      �� 51      .R�52      �� �� 53   "   �6&   �� *Flight.DepartureCity+ �� Mode$ OneWay=� �� 54   "   $	� - ��6&    �� 55   "   �6&   �� *Flight.ArrivalCity+$ OneWay=�56      57      59        5:      $ Мест��6%   $
 Auto 6U    63   �� *NumberOfSeats+6U     5;      $ 
Сумма��6%   $
 Auto 6l    64   �� *
TotalPrice+ �� StringFormat$ 
{0:N2} ₽��6l     5<      $ Дата��6%   $
 Auto 6r    63   �� *BookingDate+$ dd.MM.yyyy HH:mm��6r     5=      $ Статус��6%   $
 Auto 6P    65   �� *Status+6P     5>      $
 Auto 6)    5?      �� 5@      .R�5A      �� -   5        $��Отменить��5A   %   y�5B   %   �� *Status+ �� 	Converter# [  5C      5E      5G      5H      5I      