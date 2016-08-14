# Xaml-Adaptive-Properties
This project allows you to easily create responsive xaml code using directly extra properties like X:XS.Column="1", X:XS.Row="3",X:XS.Margin="5,2,8,10", ... This package exists for UWP Win10, Universal App 8.1 and WPF!

Code Sample:

< Grid X:XS.AdaptiveGrid="True">
	<Grid.RowDefinitions>
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
	</Grid.RowDefinitions>
	<Grid.ColumnDefinitions>
		<ColumnDefinition Width="200"/>
		<ColumnDefinition/>
	</Grid.ColumnDefinitions>

	<X:XS.RowDefinitions>
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
		<RowDefinition Height="Auto" />
	</X:XS.RowDefinitions>
	<X:XS.ColumnDefinitions>
		<ColumnDefinition/>
	</X:XS.ColumnDefinitions>
	
	<TextBlock Margin="10" Grid.Row="1" Grid.Column="0"  HorizontalAlignment="Right"
			 X:XS.Margin="2"  X:XS.Row="2" X:XS.HorizontalAlignment="Left"
		   Text="Last Name"/>
	<TextBox Margin="10" Grid.Row="1" Grid.Column="1" 
			 X:XS.Margin="2" X:XS.Row="3"/>
	<TextBlock Margin="10" Grid.Row="2" Grid.Column="0"  HorizontalAlignment="Right"
			 X:XS.Margin="2" X:XS.Row="4" X:XS.HorizontalAlignment="Center" Text="Gender"/>
</ Grid>

