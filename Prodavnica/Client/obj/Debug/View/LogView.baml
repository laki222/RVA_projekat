   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client.ViewClient.View  5      3clr-namespace:Client.ViewModelClient.ViewModel  5          Client.View.LogView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Client.View     5      5
viewModelsclr-namespace:Client.ViewModel     5        � DataContext  5
      #  Client.ViewModel.LogViewModel  5   
   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid     
IsReadOnly .    AutoGenerateColumns .     SelectionMode$ Single=�5      ��5      �� *	Entries+   RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5      �� 5       �� Binding 6&   �� *Level+ �� Value 06@   .a�5      �� 	 �� Property	 � ��
 �� Value

 � � �6;   �� 5      	 � ��

 �����6;   5      �� 5       6&   �� *Level+ 16@   .a�5      �� 	 � ��

 � � �6;   5      �� 5       6&   �� *Level+ 26@   .a�5      �� 	 � ��

 � ���6;   5       �� 5!       6&   �� *Level+ 36@   .a�5"      �� 	 � ��

 �  ��6;   5#      �� 5$       6&   �� *Level+ 46@   .a�5%      �� 	 � ��

 �  ��6;   �� 5&      	 � ��

 �����6;   5'      5)         Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ Level��5-         Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 5.      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5/      �� *Level+  50      $
 Time��51      $ 1* 52       53      �� *Time+  54      $ Message��55      $ 4* 56       57      �� *	Message+59      5:      5;      