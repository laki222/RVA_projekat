   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client.ViewClient.View  5      3clr-namespace:Client.ViewModelClient.ViewModel  5          Client.View.AdminView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Client.View     5      5
viewModelsclr-namespace:Client.ViewModel     5        � DataContext  5
      %  Client.ViewModel.AdminViewModel  5   
   .��5      � .+�5   
   �� .+�5      �� $��
Username: ��6   �� 5      $	��200��5      $
��Left=�5      $
��5, 0q�5      ��5      ��  �� Path$ UsernameTextBox� �� Mode$ TwoWay=��� 5      $��
Password: ��6   G� 5      -   5        $��passwordBox��5      $	��200��5      $
��Left=�5      $
��5, 0q�5      5      �� 5      $��First name: ��6   �� 5      $	��200��5      $
��Left=�5      $
��5, 0q�5      ��5       �� $ FirstNameTextBox�$ TwoWay=��� 5!      $��Last name: ��6   �� 5"      $	��200��5#      $
��Left=�5$      $
��5, 0q�5%      ��5&      �� $ LastNameTextBox�$ TwoWay=��� 5'      $��Create account��5(      $	��200��5)      $
��Left=�5*      $��5q�5+      ��5,      �� *CreateUserCommand+��5-      ��  �� ElementName$ passwordBox���� 5.      
���  ����5/      �� *	ErrorText+50      51      52      53      