   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client.ViewClient.View  5      3clr-namespace:Client.ViewModelClient.ViewModel  5          Client.View.LoginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Client.View     5      5
viewModelsclr-namespace:Client.ViewModel     5        � DataContext  5      %   Client.ViewModel.LoginViewModel  5   
   .��5      � .+�5   
   �� $	��200��6   $��Center=�6!   .+�5      �� $��36(�6   $
��Bold"�6"   $��Center=�64   
�������.��6c   Log in6k   �� 5      $��11(�6   $
��Bold"�6"   .��64   
Username6>   �� 5      $
��0, 2q�6   ��6$   �� *
Username+�� 5      $��11(�6   $
��Bold"�6"   .��64   
Password6>   G� 5      -   5        $��passwordBox��5      $
��0, 2q�6-   �� 5      $
��0, 2q�6   ��6#   �� *LoginCommand+��6D   ��  �� ElementName$ passwordBox��.��6y   Log in6�   �� 5      
���  ����6%   �� *	ErrorText+5      5      5      