   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Client.ViewClient.View  5   	   3clr-namespace:Client.ViewModelClient.ViewModel  5      &     Client.View.AddProductToBillView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Client.View     5   	   5
viewModelsclr-namespace:Client.ViewModel     5      -   5        $��newProductWindow��5
        � Title$  Add product to bill��5	   	   $	��330��5      $	��270��5      $	��330��5      $	��270��5       � WindowStyle$ 
ToolWindow=�5       � 
ResizeMode$ NoResize=�5       � DataContext 5      0   *Client.ViewModel.AddProductToBillViewModel  5   
   .��5      � .+�5   
   �� $��Center=�6   $��Center=�60   .+�5      �� $��Name: ��6   �� 5      $	��200��5      $
��Left=�5      $
��5, 0q�5      ��5      ��  �� Path$
 Name� �� Mode$ TwoWay=��� 5      $��Manufacturer: ��6   �� 5      $	��200��5      $
��Left=�5       $
��5, 0q�5!      ��5"      �� $ Manufacturer�$ TwoWay=��� 5#      $��Price: ��6   �� 5$      $	��200��5%      $
��Left=�5&      $
��5, 0q�5'      ��5(      �� $ Price�$ TwoWay=��� 5)      $��Add product��5*      $	��200��5+      $
��Left=�5,      $��5q�5-      ��5.      �� *AddProductCommand+��5/      ��  �� ElementName$ newProductWindow���� 50      
���  ����51      �� *ErrorMessage+52      53      54      55      