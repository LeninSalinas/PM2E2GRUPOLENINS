using Plugin.AudioRecorder;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PM2E2GRUPOLENINS.Modelo;
using PM2E2GRUPOLENINS.Vista;
using System;
using System.IO;
using System.Threading.Tasks;
using Xam.Forms.VideoPlayer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E2GRUPOLENINS
{
    public partial class MainPage : ContentPage
    {
        private MediaFile video; 
        AudioRecorderService recorder;
        AudioPlayer player;
        public MainPage()
        {
            InitializeComponent();

            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
        }
        async void Record_Clicked(object sender, EventArgs e)
        {
            // Solicitar permiso para acceder al micrófono
            var status = await Permissions.RequestAsync<Permissions.Microphone>();

            //do{

                switch (status)
                {
                    case PermissionStatus.Granted:
                        await RecordAudio();
                        break;

                    case PermissionStatus.Denied:
                        // No se otorgó permiso para acceder al micrófono
                        // Manejar el escenario en consecuencia
                        await DisplayAlert("Alerta", "Por favor, otorgue el permiso para grabar el audio", "Aceptar");
                        await Permissions.RequestAsync<Permissions.Microphone>();
                        break;
                }
            //}while (status == PermissionStatus.Granted);
        }

        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording) //Record button clicked
                {
                    recorder.StopRecordingOnSilence = TimeoutSwitch.IsToggled;

                    RecordButton.IsEnabled = false;
                    PlayButton.IsEnabled = false;

                    //start recording audio
                    var audioRecordTask = await recorder.StartRecording();

                    RecordButton.Text = "Stop Recording";
                    RecordButton.IsEnabled = true;

                    await audioRecordTask;

                    RecordButton.Text = "Record";
                    PlayButton.IsEnabled = true;
                }
                else //Stop button clicked
                {
                    RecordButton.IsEnabled = false;

                    //stop the recording...
                    await recorder.StopRecording();

                    RecordButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }

        void Play_Clicked(object sender, EventArgs e)
        {
            PlayAudio();
        }

        void PlayAudio()
        {
            try
            {
                var filePath = recorder.GetAudioFilePath();

                if (filePath != null)
                {
                    PlayButton.IsEnabled = false;
                    RecordButton.IsEnabled = false;

                    player.Play(filePath);
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }

        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
        }
        public byte[] GetVideoBytes()
        {
            if (video != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    System.IO.Stream stream = video.GetStream();
                    stream.CopyTo(memoryStream);
                    byte[] videoBytes = memoryStream.ToArray();

                    return videoBytes;
                }
            }

            return null;
        }
        public byte[] GetAudioByteArray()
        {
            if (recorder != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    System.IO.Stream stream = recorder.GetAudioFileStream();
                    stream.CopyTo(memoryStream);
                    byte[] audioBytes = memoryStream.ToArray();

                    return audioBytes;
                }
            }

            return null;
        }


        private async void btnVideo_Clicked(object sender, EventArgs e)
        {
            var mediaOptions = new StoreVideoOptions
            {
                SaveToAlbum = true,
                Directory = "MYAPP",
                Name = "Video.mp4"
            };

            video = await CrossMedia.Current.TakeVideoAsync(mediaOptions);

            if (video != null)
            {
                Video.Source = new FileVideoSource { File = video.Path };
                Video.Play();
            }
        }


        private void Locl_PositionChanged(object sender, PositionEventArgs e)
        {
            double latitude = e.Position.Latitude;
            latitud.Text = latitude.ToString();

            double longitude = e.Position.Longitude;
            longitud.Text = longitude.ToString();
        }

        private async void btnSavedLocation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageListsSitios());
        }
        private void latitud_Unfocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                latitud.Unfocus();
            }
        }

        private async void btngetubicacion_Clicked_2(object sender, EventArgs e)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            if (status == PermissionStatus.Granted)
            {
                var conectividad = Connectivity.NetworkAccess;
                var locl = CrossGeolocator.Current;

                if (conectividad == NetworkAccess.Internet)
                {
                    if (locl != null)
                    {
                        locl.PositionChanged += Locl_PositionChanged;
                        if (!locl.IsListening)
                        {
                            await locl.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                        }

                        var posicion = await locl.GetPositionAsync();

                        double latitude = posicion.Latitude;
                        latitud.Text = latitude.ToString();

                        double longitude = posicion.Longitude;
                        longitud.Text = longitude.ToString();
                    }
                }
                else
                {
                    var posicion = await locl.GetLastKnownLocationAsync();

                    if (posicion == null)
                    {
                        // Mostrar imagen o icono que indica que el GPS no está activo
                        // Por ejemplo, puedes cambiar la imagen de un control Image
                        //psImage.Source = "gps_off.png";

                        // Mostrar una alerta indicando que el GPS no está activo o no se pudo obtener la última ubicación conocida
                        await DisplayAlert("Error", "No se pudo obtener la ubicación. Asegúrate de tener activado el GPS.", "Aceptar");
                    }
                    else
                    {
                        double latitude = posicion.Latitude;
                        latitud.Text = latitude.ToString();

                        double longitude = posicion.Longitude;
                        longitud.Text = longitude.ToString();
                    }
                }

            }

        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var sitio = new Sitio
                {
                    Descripcion = descripcion.Text,
                    Latitud = Convert.ToDouble(latitud.Text),
                    Longitud = Convert.ToDouble(longitud.Text),
                    VideoDigital = GetVideoBytes(),
                    AudioFile = GetAudioByteArray()
                };
                Modelo.Msg resultado = await Controlador.SitioController.CreateSitio(sitio);
                if (resultado != null)
                {
                    await DisplayAlert("Aviso", resultado.message.ToString(), "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Aviso", ex.Message, "OK");
            }


        }
    }
}
