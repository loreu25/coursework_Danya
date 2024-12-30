﻿#pragma checksum "..\..\..\..\Dialogs\BookingDialog.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F1B8DBF60015D45D4C51921C0AD6D5D33C7E2F69"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TicketBookingSystem.Dialogs {
    
    
    /// <summary>
    /// BookingDialog
    /// </summary>
    public partial class BookingDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbFlights;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtFlightInfo;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtAvailableSeats;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTicketPrice;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPassengerName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPassengerContact;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNumberOfSeats;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTotalPrice;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBook;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Dialogs\BookingDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TicketBookingSystem;component/dialogs/bookingdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Dialogs\BookingDialog.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmbFlights = ((System.Windows.Controls.ComboBox)(target));
            
            #line 33 "..\..\..\..\Dialogs\BookingDialog.xaml"
            this.cmbFlights.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbFlights_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtFlightInfo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtAvailableSeats = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtTicketPrice = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtPassengerName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtPassengerContact = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtNumberOfSeats = ((System.Windows.Controls.TextBox)(target));
            
            #line 57 "..\..\..\..\Dialogs\BookingDialog.xaml"
            this.txtNumberOfSeats.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNumberOfSeats_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtTotalPrice = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.btnBook = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\..\Dialogs\BookingDialog.xaml"
            this.btnBook.Click += new System.Windows.RoutedEventHandler(this.btnBook_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\..\Dialogs\BookingDialog.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

