   M S B A M L   `   `   `  ����A  =PPGVMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PPGVMSPPGVMS  5   	       PPGVMS.Views.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:PPGVMS     5   	   -   5          � Title$/  )Pikes Peak Gamers Vault Management System��5   	   $	��770��6;   $
��1005��6H    � Icon$+ %/PPGVMS;component/Resources/vault.png��6U    J� InputBindings 5
      �� 5   
   
 �� Key$ L��6    �� 	Modifiers$
 Ctrlf�6    �� Command 6.   �� *LogoutCommand+�� 5   
   $ O��6   $
 Ctrlf�6    6.   �� *LoadCheckoutCommand+�� 5   
   $ I��6   $
 Ctrlf�6    6.   �� *LoadCheckinCommand+.��5      ]� -   5        $��mainPane��5      .+�5   
   ]� $
��Left=�6   $	��Top=�6/   ��. $
��auto��6Y   .+�5      `� $
��Left=�6   .�5      c� .@�5      �� -   5        $��btnCheckOut��5       � ToolTip$ Ctrl+O��60   ��.  ��6A   �� *LoadCheckoutCommand+{�6i   �� *
SignedIn+.��5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $��/Resources/checkout.png��6$   $��32��6E   $��32��6P   $��5,0,0,0q�6\   �� 5      $��Center=�6(   $��	Check Out��6C   5      5      �� 5      -   5        $��
btnCheckIn��5      $ Ctrl+I��6/   ��.  ��6@   �� *LoadCheckinCommand+{�6g   �� *
SignedIn+.��5       �� $ 
Horizontal=�6%   .+�5!      �� $��/Resources/checkin.png��6$   $��32��6D   $��32��6O   $��5,0,0,0q�6[   �� 5"      $��Center=�6(   $��Check In��6C   5#      5$      �� 5%      -   5        $��	btnLogout��5%      $ Ctrl+L��6.   ��.  ��6?   �� *LogoutCommand+{�6a   �� *
SignedIn+.��5&      �� $ 
Horizontal=�6%   .+�5'      �� $��/Resources/lock.png��6$   $��32��6A   $��32��6L   $��5,0,0,0q�6X   �� 5(      $��Center=�6(   $��Log Out��6C   5)      5*      �� 5+      $��Center=�6   $��10,0,0,0q�67   .��6I   
Librarian:6V   �� 5,      -   5         ��Name$ lblCurrentLibrarian��5,      $��Center=�69   $��10,0,0,0q�6T   ��6f   �� *CurrentLibrarian+	 �� Mode$	 OneWay=�5-      5/      50      ]� 51   
   -   5        $��content��51      $
��auto��6#   ��. $��Stretch=�6E   .+�53      �� $ Vertical=�6   $��0,0,0,-102.667q�60   .+�54      ]� -   5        $��
userSearch��54      $	��200��6.   $
��Left=�6;   $��Stretch=�6Q   $��	10,10,0,0q�6o   .+�55      � 
 � RowDefinitions
 56      �� 57      $
��Auto �6,   �� 58      $
��Auto �6,    � ColumnDefinitions 5:      �� 5;      $��75 �6/   �� 5<      $	��200 �6/   �� 5=      $	��500 �6/   .+�5@      �� $��Center=�6    $
��Left=�6;   $��0��6V   $��75��6c   $��0��6n   $��1��6~   .��6�   User Search:6�   �� 5A      -	   5        $��txtUserSearchName��5A   �   $��3��6"   $��0��6/   $��1��6<   $��2��6L   $	��160��6`   $
��Left=�6l   $��Center=�6�   6�   �� 5B      $��1��6'   $��0��64   $��3��6D   $
��Auto=�6X   $
��Auto=�6{   $
��Auto��6�   .��5C      &   System.Windows.Controls.DataGrid  -
   5        $��grdUserSearch��5C   9   ��.     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    AutoGenerateColumns .  $	��175��6�   $	��750��6�   $
��Left=�5D   '   ��6B   �� *UserSearchData+   Columns 5E   "   0  *System.Windows.Controls.DataGridTextColumn  5F   &   ,  &System.Windows.Controls.DataGridColumn   Header$
 User��69      Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *
username+6d     5G   &   $ User Barcode��69   $	 125 6p    6O   �� *user_barcode+6p     5H   &   $ Game Checked Out��69   $	 275 6p    6S   �� *
gamename+6p     5I   &   $ Game Barcode��69   $	 125 6p    6O   �� *game_barcode+6p   5K      5L      5M      5N      ]� 5O      -   5        $��
gameSearch��5O      $	��200��6.   $
��Left=�6;   $��Stretch=�6Q   $��	10,10,0,0q�6o   .+�5P      � 
 5Q      �� 5R      $
��Auto �6,   �� 5S      $
��Auto �6,    5U      �� 5V      $��80 �6/   �� 5W      $	��320 �6/   �� 5X      $	��495 �6/   .+�5[      �� $��Center=�6    $��0��6;   $��0��6H   $��2��6X   .��6l   Game Search:6z   �� 5\      -   5        $��txtGameSearchName��5\   �   $��4��6"   $��0��6/   $��1��6<   $
��Left=�6L   $��Center=�6g   $��18��6�   $	��300��6�   �� 5]      $��1��6'   $��0��64   $��3��6D   $
��Auto=�6X   $
��Auto=�6{   $
��Auto��6�   .��5^        -   5        $��grdGameSearch��5^   �   ��.   .   .   . $	��150��6|   $	��750��6�   $
��Left=�6�    .  ��5_   C   �� *GameSearchData+ 5`   "     5a   &   $	 300 69   $
 Game��6^    6E   �� *name+6^     5b   &   $ 70 69   $ Instance��6a    6D   �� *
instance+6a     5c   &   $ 90 69   $ Status��6_    6D   �� *status+6_     5d   &   $	 120 69   $ Vault Location��6b    6E   �� *
location+6b     5e   &   $ 50 69   $
 Sort��6e    6D   �� *sorting_code+6e     5f   &   $	 125 69   $ Barcode��6a    6E   �� *	barcode+6a   5h      5i      5j      5k      ]� 5l      -   5        $��checkoutHistory��5l      $	��210��63   $��Bottom=�6@   $��Stretch=�6X   $��	10,10,0,0q�6v   .+�5m      � 
 5n      �� 5o      $
��Auto �6,   �� 5p      $
��Auto �6,    5r      �� 5s      $
��Auto �6/   �� 5t      $
��Auto �6/   .+�5v      �� $��0��6    .��6-   Check Out History6@   �� 5w      $��1��6'   $
��Auto=�64   $
��Auto=�6W   $
��Auto��6|   .��5x        -   5        $��grdCheckoutHistory��5y   E   ��.   .   .   . $	��975��5x   |   $	��180��6�   $��Stretch=�5y   '    .  ��5z   '   �� *HistoryData+ 5{   "     5|   &   $	 210 69   $
 User��6E    6S   �� *	user_name+  5}   &   $	 210 69   $
 Game��6E    6S   �� *	game_name+  5~   &   $	 200 69   $ Checkout Time��6E    6\   �� *checked_out+  5   &   $	 200 69   $ Checkin Time��6E    6[   �� *
checked_in+  5�   &   $	 125 69   $ Barcode��6E    6V   �� *	barcode+5�      5�      5�      5�      5�      5�      5�      5�      