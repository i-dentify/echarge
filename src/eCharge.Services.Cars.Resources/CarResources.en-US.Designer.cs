namespace ECharge.Services.Cars.Resources {
    using System;
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CarResources_en_US {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CarResources_en_US() {
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ECharge.Services.Cars.Resources.CarResources_en_US", typeof(CarResources_en_US).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string AddCar {
            get {
                return ResourceManager.GetString(nameof(AddCar), resourceCulture);
            }
        }

        public static string AddNewCar {
            get {
                return ResourceManager.GetString(nameof(AddNewCar), resourceCulture);
            }
        }

        public static string BatteryCapacity {
            get {
                return ResourceManager.GetString(nameof(BatteryCapacity), resourceCulture);
            }
        }
        
        public static string CarAlreadyExists {
            get {
                return ResourceManager.GetString(nameof(CarAlreadyExists), resourceCulture);
            }
        }
                
        public static string CarDetails {
            get {
                return ResourceManager.GetString(nameof(CarDetails), resourceCulture);
            }
        }

        public static string CarDetails_Name {
            get {
                return ResourceManager.GetString(nameof(CarDetails_Name), resourceCulture);
            }
        }

        public static string CarIdRequired {
            get {
                return ResourceManager.GetString(nameof(CarIdRequired), resourceCulture);
            }
        }
        
        public static string CarNameRequired {
            get {
                return ResourceManager.GetString(nameof(CarNameRequired), resourceCulture);
            }
        }
        
        public static string CarNotFound {
            get {
                return ResourceManager.GetString(nameof(CarNotFound), resourceCulture);
            }
        }
        
        public static string ConfirmDeleteCar {
            get {
                return ResourceManager.GetString(nameof(ConfirmDeleteCar), resourceCulture);
            }
        }

        public static string EditCar_Name {
            get {
                return ResourceManager.GetString(nameof(EditCar_Name), resourceCulture);
            }
        }

        public static string InvalidBatteryCapacity {
            get {
                return ResourceManager.GetString(nameof(InvalidBatteryCapacity), resourceCulture);
            }
        }
        
        public static string InvalidCarData {
            get {
                return ResourceManager.GetString(nameof(InvalidCarData), resourceCulture);
            }
        }
        
        public static string MyCars {
            get {
                return ResourceManager.GetString(nameof(MyCars), resourceCulture);
            }
        }
        
        public static string NameOfTheCar {
            get {
                return ResourceManager.GetString(nameof(NameOfTheCar), resourceCulture);
            }
        }
        
        public static string NoCarsAssignedAddNew {
            get {
                return ResourceManager.GetString(nameof(NoCarsAssignedAddNew), resourceCulture);
            }
        }
        
        public static string NotOwnerOfCar {
            get {
                return ResourceManager.GetString(nameof(NotOwnerOfCar), resourceCulture);
            }
        }
        
        public static string YesDeleteCar {
            get {
                return ResourceManager.GetString(nameof(YesDeleteCar), resourceCulture);
            }
        }
    }
}
