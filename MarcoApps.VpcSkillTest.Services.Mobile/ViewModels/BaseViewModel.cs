namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private string? title;

        /// <summary>
        /// Indica si una operación está en curso.
        /// </summary>
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        /// <summary>
        /// Título de la página o del ViewModel.
        /// </summary>
        public string? Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        /// <summary>
        /// Implementación del evento para notificar cambios en las propiedades.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Método para notificar un cambio en una propiedad.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Método para asignar un valor a una propiedad y notificar el cambio.
        /// </summary>
        /// <typeparam name="T">Tipo de la propiedad.</typeparam>
        /// <param name="backingStore">Referencia a la propiedad de respaldo.</param>
        /// <param name="value">Nuevo valor para la propiedad.</param>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <returns>True si el valor cambió, de lo contrario False.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName!);
            return true;
        }
    }
}