   M S B A M L   `   `   `  ����A  =PPGVMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PPGVMS.ViewsPPGVMS.Views  5   	       PPGVMS.Views.CheckinForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PPGVMS.Views     5   	   -   5          � Title$  Checkout��5   	    � Icon$- '/PPGVMS;component/Resources/checkin.png��6   $��124.106��6J   $	��776��6[    � 
ResizeMode$ NoResize=�6g    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�6�    � SizeToContent$ Height=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��65 �6    � ColumnDefinitions 5   
   �� 5      $	��185 �6   �� 5      $	��275 �6   �� 5      $	��275 �6   .+�5   
   �� $��Game Barcode:��6   $��0��6(   $��0��65   $��24(�6E   $
��Bold"�6S   �� 5   
   -   5        $��txtGameBarcode��5      $��0��6(   $��0��65   $��1��6B   $��0,5,0,5q�6R   $��24(�6c   6q   �� 5   
   -   5        $��lblGameSearchResult��5      $��Enter Game Barcode...��6+   $��0��6K   $��2��6X   $��0,5,0,5q�6h   $��24(�6y   �� 5   
   -   5        $��lblStatusMessage��5      $
��Temp��6(   $	��100��67   $	��550��6D   $��0q�6P   $��1��6d   $��1��6q   $��0��6~   $��22(�6�   $��2��6�   $y�	Collapsed=�6�   �� 5   
   $��2��6   $��0��6   6+   5      5      