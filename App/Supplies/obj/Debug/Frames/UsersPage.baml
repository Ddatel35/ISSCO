   M S B A M L   `   `   `  ����C  ?Supplies, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Supplies.FramesSupplies.Frames  5          Supplies.Frames.UsersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Supplies.Frames     5        N� Title$  	UsersPage��5	      .��5      � 
������� � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��650* �6   .+�5   
   �� $
��Left=�6   $��0��6/   $��20,0,0,0q�6<   $
��Wrap=�6N   $��Пользователи��6b   $��Center=�6v   $��18��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DGrid��5      $��1��6"   $��5,0,5,5q�6/      AutoGenerateColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Логин��6?      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *login+6N     5      $ Пароль��6B   $ * 6R    6%   �� *
password+6R     5      $ Роль��6H   $ * 6V    6%   �� *Roles.roleName+6V   5      �� 5   
   -   5        	 ��Name$	 DelBtn��5      $��0��6!   $��Удалить��6.   $��75��6Q   $��0,0,10,0q�6\   $��Right=�6n   $��Center=�6�   �� 5   
   -   5        $	 EditBtn��5      $��0��6"   $ ��Редактировать��6/   $	��120��6Y   $��0,0,95,0q�6e   $��Right=�6w   $��Center=�6�   �� 5   
   -   5        $	 AddBtn��5      $��0��6!   $��Добавить��6.   $��85��6R   $��	0,0,225,0q�6]   $��Right=�6p   $��Center=�6�   5      5       