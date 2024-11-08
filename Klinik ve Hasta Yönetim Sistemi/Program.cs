using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klinik_ve_Hasta_Yönetim_Sistemi
{
    public class Randevu
    {
        public string randevunumarasi;
        public string klinik;
        public string doktoradi;
        public string randevutarihi;
        public string durumu;
        public Hasta hasta;

        public Randevu(string randevunumarasi, string klinik, string doktoradi, string tarihi, string durumu)
        {
            this.klinik = klinik;
            this.doktoradi = doktoradi;
            this.randevutarihi = tarihi;
            this.randevunumarasi = randevunumarasi;
            this.durumu = durumu;
        }
    }
    public class Hasta
    {
        public string hastaadi;
        public string hastasoyadi;
        public string tckimlik;

        public Hasta(string hastaadi, string hastasoyadi, string tckimlik)
        {
            this.hastaadi = hastaadi;
            this.hastasoyadi = hastasoyadi;
            this.tckimlik = tckimlik;
        }
    }

    public class Program
    {
        static Randevu[] randevu_listesi;
        static Hasta[] hasta_listesi;
        static int randevu_sayisi = 0;
        static int hasta_sayisi = 0;
        private static bool hasta_bulundu;

        static void Main(string[] args)
        {
            randevu_listesi = new Randevu[100];
            hasta_listesi = new Hasta[100];
            while (true)
            {
                Console.WriteLine(".:: Hastane ::.");
                Console.WriteLine("1 - Doktorun Müsaitlik Durumu");
                Console.WriteLine("2 - Randevu Alınabilecek Saatleri Listeleme");
                Console.WriteLine("3 - Hasta Ekleme");
                Console.WriteLine("4 - Hasta Listeleme");
                Console.WriteLine("5 - Randevu Alma");
                Console.WriteLine("6 - Randevu İptal");
                Console.WriteLine("7 - Çıkış");
                string girilen_değer = Console.ReadLine();

                int secim = Convert.ToInt32(girilen_değer);

                switch (secim)
                {
                    case 1:
                        Console.WriteLine("Randevu Ekleme İşlemi");
                        Randevu randevu = RandevuEkle();
                        randevu_listesi[randevu_sayisi] = randevu;
                        randevu_sayisi = randevu_sayisi + 1;
                        break;
                    case 2:
                        Console.WriteLine("Doktorumuzun Müsaitlik Durumları");
                        RandevuListele();
                        break;
                    case 3:
                        Console.WriteLine("Hasta Ekleme İşlemi");
                        Hasta hasta = HastaEkle();
                        hasta_listesi[hasta_sayisi] = hasta;
                        hasta_sayisi = hasta_sayisi + 1;
                        break;
                    case 4:
                        Console.WriteLine("Kayıtlı Hasta Listeleme İşlemi");
                        HastaListele();
                        break;
                    case 5:
                        Console.WriteLine("Randevu Alma İşlemi");
                        RandevuAl();
                        break;
                    case 6:
                        Console.WriteLine("Randevu İptal İşlemi");
                        Randevuİptal();
                        break;
                    case 7:
                        Cikis();
                        break;
                    default:
                        break;
                }
            }
        }
        public static Randevu RandevuEkle()
        {
            Random random = new Random();
            string randevunumarasi = random.Next(1000, 10000).ToString();
            Console.WriteLine("Klinik adını giriniz: ");
            string klinik = Console.ReadLine();
            Console.WriteLine("Doktorun adını ve soyadını giriniz: ");
            string doktoradi_soyadi = Console.ReadLine();
            Console.WriteLine("Randevu tarihini giriniz: ");
            string randevutarihi = Console.ReadLine();

            Randevu randevu = new Randevu(randevunumarasi, klinik, doktoradi_soyadi, randevutarihi, "Boş");

            Console.WriteLine($"{randevunumarasi} numaralı {klinik} {doktoradi_soyadi} randevu alınabilecek randevulara eklendi. ");
            Console.WriteLine($"==============================\n");
            return randevu;
        }
        public static void RandevuListele()
        {
            for (int i = 0; i < randevu_sayisi; i++)
            {
                if (randevu_listesi[i].durumu != "Boş değil")
                {
                    Console.WriteLine($"Durumu: {randevu_listesi[i].durumu}");
                }
                else
                {
                    Console.WriteLine($"Durumu: {randevu_listesi[i].durumu} ({randevu_listesi[i].hasta.hastaadi} {randevu_listesi[i].hasta.hastasoyadi}");
                }
            }
            Console.WriteLine($"==============================\n");
        }
        public static Hasta HastaEkle()
        {
            Console.WriteLine("Kaydı olacak hastanın adını giriniz: ");
            string hastaadi = Console.ReadLine();
            Console.WriteLine("Kaydı olacak kişinin soyadını giriniz: ");
            string hastasoyadi = Console.ReadLine();
            Console.WriteLine("Hasta Kimlik No gir: ");
            string kimlik = Console.ReadLine();
            Hasta hasta = new Hasta(hastaadi, hastasoyadi, kimlik);
            Console.WriteLine($"{hastaadi} {hastasoyadi}, kayıt listesine eklendi.");
            Console.WriteLine($"==============================\n");

            return hasta;
        }
        public static void HastaListele()
        {
            for (int i = 0; i < hasta_sayisi; i++)
            {
                Console.WriteLine($"{i + 1}. Üye: ");
                Console.WriteLine($"Ad: {hasta_listesi[i].hastaadi}");
                Console.WriteLine($"Soyad: {hasta_listesi[i].hastasoyadi}");
                Console.WriteLine($"Kimlik No: {hasta_listesi[i].tckimlik}");
            }
            Console.WriteLine($"==============================\n");
        }
        public static void RandevuAl()
        {
            Console.WriteLine("Randevu alınacak numarayı giriniz");
            string oluşturulanrandevu = Console.ReadLine();

            bool bos = false;
            int bulunan_randevu_indisi = 0;

            for (int i = 0; i < randevu_sayisi; i++)
            {
                if (randevu_listesi[i].randevunumarasi == oluşturulanrandevu)
                {
                    bos = true;
                    bulunan_randevu_indisi = i;
                    break;
                }
            }
            if (bos == false)
            {
                Console.WriteLine($"{oluşturulanrandevu} doktor randevu vermiyor!");
            }
            else
            {
                Console.WriteLine("Hasta kimlik numarasını gir: ");
                string hasta_kisinin_kimlik_no = Console.ReadLine();

                bool kayıtlı_hasta_bulundu = false;
                int bulunan_hasta_indisi = 0;

                for (int i = 0; i < hasta_sayisi; i++)
                {
                    if (hasta_listesi[i].tckimlik == hasta_kisinin_kimlik_no)
                    {
                        hasta_bulundu = true;
                        bulunan_hasta_indisi = i;
                        break;
                    }
                }
                if (hasta_bulundu == false)
                {
                    Console.WriteLine($"{hasta_kisinin_kimlik_no} kimlikli kayıtlı hasta bulunamadı. Yeni kayt oluşturun!");
                }
                else
                {
                    randevu_listesi[bulunan_randevu_indisi].durumu = "Dolu";
                    randevu_listesi[bulunan_randevu_indisi].hasta = hasta_listesi[bulunan_hasta_indisi];
                    Console.WriteLine($"{randevu_listesi[bulunan_hasta_indisi].randevunumarasi}'lı randevu, {hasta_listesi[bulunan_hasta_indisi].hastaadi} {hasta_listesi[bulunan_hasta_indisi].hastasoyadi} tarafından alınmıştır.");
                }
            }
            Console.WriteLine($"==============================\n");
        }
        public static void Randevuİptal()
        {
            Console.WriteLine("İptal etmek istediğiniz randevunun numarasını giriniz: ");
            string iptal_edilmek_istenen_randevunumarasi = Console.ReadLine();

            bool randevu_bulundu = false;
            int bulunan_randevu_indisi = 0;


            for (int i = 0; i < randevu_sayisi; i++)
            {
                if (randevu_listesi[i].randevunumarasi == iptal_edilmek_istenen_randevunumarasi)
                {
                    randevu_bulundu = true;
                    bulunan_randevu_indisi = i;
                    break;
                }
            }

            if (!randevu_bulundu)
            {
                Console.WriteLine($"{iptal_edilmek_istenen_randevunumarasi} numaralı randevu bulunamadı!");
            }
            else
            {
                if (randevu_listesi[bulunan_randevu_indisi].durumu == "Dolu")
                {
                    randevu_listesi[bulunan_randevu_indisi].durumu = "Boş";
                    randevu_listesi[bulunan_randevu_indisi].hasta = null;

                    Console.WriteLine($"{iptal_edilmek_istenen_randevunumarasi} numaralı randevu iptal edilmiştir!");
                }
                else
                {
                    Console.WriteLine($"{iptal_edilmek_istenen_randevunumarasi} numaralı randevu zaten alınmamış.");
                }
            }
            Console.WriteLine($"==============================\n");
        }

        public static void Cikis()
        {
            System.Environment.Exit(0);
        }
    }
}
