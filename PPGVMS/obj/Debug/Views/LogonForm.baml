   M S B A M L   `   `   `  ����A  =PPGVMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PPGVMS.ViewsPPGVMS.Views  5   	       PPGVMS.Views.LogonForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PPGVMS.Views     5   	     � Title$  PPGVMS Logon��5   	   $��95��6   $	��375��6*    � Icon$* $/PPGVMS;component/Resources/lock.png��66    � WindowStartupLocation$ CenterOwner=�6b    � 
ResizeMode$ NoResize=�6�    � ShowInTaskbar .  .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $	��250 �6   .+�5   
   �� $��Librarian Barcode:��6   $��0��6-   $��0��6:   �� 5   
   -   5        $��txtUserName��5      $��20��6�   $
��Left=�6�   $	��220��6�   $��0��6�   $��1��6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $��5,5,5,5q�6.   $��0��6?   $��2��6O   $��2��6\   $��Center=�6p   .+�5      �� -   5        $��	btnCancel��5      $��Cancel��6&    �� IsCancel . $��65��6G   5      5      5      