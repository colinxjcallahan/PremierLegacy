using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace eRIS
{
    public class Menu : INotifyPropertyChanged
    {
        public Menu()
        {
            this._submenu = new ObservableCollection<Menu>();
        }

        private string _header;
        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged("Header");
                }
            }
        }        
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private String[] _role;
        public String[]  Role
        {
            get
            {
                return _role;
            }

            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged("Role");
                }
            }
        }
        private System.Windows.Visibility _visibility;
        public System.Windows.Visibility Visibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                if (_visibility != value)
                {
                    _visibility = value;
                    OnPropertyChanged("Visibility");
                }
            }
        }
        private ObservableCollection<Menu> _submenu;
        public ObservableCollection<Menu> SubMenu
        {
            get
            {
                return _submenu;
            }

            set
            {
                if (_submenu != value)
                {
                    _submenu = value;
                    OnPropertyChanged("SubMenu");
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class Menus : ObservableCollection<Menu>
    {
        public Menus()
        {
            this.Add(new Menu()
            {
                Header = "Administration",
                Name = "MENU1",
                Role = new String[] { "Admin" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "Set Login Page Images",
                    Name = "LoginImagesPane",
                    Role = new String[] { "Admin" } ,
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Set Permissions",
                    Name = "PermissionsPane",
                    Role = new String[] { "Admin" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "View Service Messages",
                    Name = "MessagePane",
                    Role = new String[] { "Admin" },
                    Visibility = System.Windows.Visibility.Visible
                } ,
                new Menu()
                {
                    Header = "View User Activity",
                    Name = "UserActivityPane",
                    Role = new String[] { "Admin" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Toggle Log Visibility",
                    Name = "ToggleLogVisibility",
                    Role = new String[] { "Admin" },
                    Visibility = System.Windows.Visibility.Visible
                }
            }
            });
            this.Add(new Menu()
            {
                Header = "Charts/Graphs",
                Name = "MENU2",
                Role = new String[] { "Manager", "Radiologist" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "First/Last Appointment",
                    Name = "FirstLastStudyPane",                
                    Role = new String[] { "Manager" },                
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Interpretation Time",
                    Name = "InterpretationTimePane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Patient Long Wait Time",
                    Name = "PatientLongWaitPane",                
                    Role = new String[] { "Manager" },                
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Patient Short Wait Time",
                    Name = "PatientShortWaitPane",                
                    Role = new String[] { "Manager" },                
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Patient Wait Time",
                    Name = "PatientWaitPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Registered Patients Per Clerk",
                    Name = "RegisteredPerClerkPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Registration Time",
                    Name = "AvgRegistrationPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Reports Coded Per Day",
                    Name = "ReportsCodedPerDayPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Scheduled Patients",
                    Name = "ScheduledPatientsPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Studies Completed Per Tech",
                    Name = "CompletedPerTechPane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Studies Read Per Day",
                    Name = "StudiesReadPerDayPane",
                    Role = new String[] { "Manager", "Radiologist" },
                    Visibility = System.Windows.Visibility.Visible
                },
                new Menu()
                {
                    Header = "Turnaround Time",
                    Name = "TurnaroundTimePane",
                    Role = new String[] { "Manager" },
                    Visibility = System.Windows.Visibility.Visible
                    }
            }
            });
            this.Add(new Menu()
            {
                Header = "Clinic",
                Name = "MENU3",
                Role = new String[] { "RISAdmin", "Clerk" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "BioImaging Reports",
                    Name = "BioImagingPane",
                    Role = new String[] { "RISAdmin", "Clerk" },
                    Visibility = System.Windows.Visibility.Visible
                }
            }
            });
            this.Add(new Menu()
            {
                Header = "Coding",
                Name = "MENU4",
                Role = new String[] { "RISAdmin", "Coder" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "Coder Worklist",
                    Name = "PhyCoderWorklistPane",
                    Role = new String[] { "RISAdmin", "Coder" },
                    Visibility = System.Windows.Visibility.Visible
                }
            }
            });

            this.Add(new Menu()
            {
                Header = "External Interface",
                Name = "MENU5",
                Role = new String[] { "RISAdmin" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "Cross Reference Tables",
                    Name = "MENU6",
                    Role = new String[] { "RISAdmin" },
                    Visibility = System.Windows.Visibility.Visible,
                    SubMenu = 
                    {
                    new Menu()
                        {
                            Header = "Ordering Physician Xref",
                            Name = "OrderingPhysicianXrefPane",
                            Role = new String[] { "RISAdmin" },
                            Visibility = System.Windows.Visibility.Visible
                        },
                    new Menu()
                        {
                            Header = "Patient Xref",
                            Name = "PatientXrefPane",
                            Role = new String[] { "RISAdmin" },
                            Visibility = System.Windows.Visibility.Visible
                        },
                    new Menu()
                        {
                            Header = "Procedure Xref",
                            Name = "ProcedureXrefPane",
                            Role = new String[] { "RISAdmin" },
                            Visibility = System.Windows.Visibility.Visible
                        },
                    new Menu()
                        {
                            Header = "Provider Xref",
                            Name = "ProviderXrefPane",
                            Role = new String[] { "RISAdmin" },
                            Visibility = System.Windows.Visibility.Visible
                        },
                    new Menu()
                        {
                            Header = "Radiologist Xref",
                            Name = "RadiologistXrefPane",
                            Role = new String[] { "RISAdmin" },
                            Visibility = System.Windows.Visibility.Visible
                        }
                    }
                },
                new Menu()
                {
                    Header = "Orders Pending",
                    Name = "OrdersPendingPane",
                    Role = new String[] { "RISAdmin" },
                    Visibility = System.Windows.Visibility.Visible
                }
            }
            });

            this.Add(new Menu()
            {
                Header = "Follow-ups",
                Name = "MENU7",
                Role = new String[] { "RISAdmin", "Coder" },
                Visibility = System.Windows.Visibility.Visible,
                SubMenu = 
            {
                new Menu()
                {
                    Header = "Coder Worklist",
                    Name = "PhyCoderWorklistPane",
                    Role = new String[] { "RISAdmin", "Coder" },
                    Visibility = System.Windows.Visibility.Visible
                }
            }
            });
        }
    }
}

