using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TrackingVaksinService.Helper;

namespace TrackingVaksinService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEBPOMService
    {

        //Autentication
        [OperationContract]
        Akun Register(Akun data);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        Akun GetAkun(string username);

        //end Auth
        
        
        /*
         * BPOM Vaksin
        * CRUD
        */

        [OperationContract]
        BPOM GetBPOM(string username);

        [OperationContract]

        IEnumerable<BPOM_Vaksin> GetVaksin();

        [OperationContract]

        BPOM_Vaksin GetVaksinDetails(string no_registrasi);

        [OperationContract]

        BPOM_Vaksin Up(BPOM_Vaksin data);

        [OperationContract]

        BPOM_Vaksin UpdateVaksin(BPOM_Vaksin data);

        [OperationContract]
        Feedback DeleteVaksin(string no_registrasi);

        /* End BPOM Vaksn CRUD  */

        /*
         * Produsen Vaksin
         * CRUD
         */

        [OperationContract]
        IEnumerable<Produsen_Vaksin> GetProdusenVaksin(string id_produsen);

        [OperationContract]
        Produsen_Vaksin GetProdusenVaksinDetails(string id_produsen, string no_registrasi);

        [OperationContract]
        Produsen_Vaksin GetProdusenVaksinByNoReg(string no_registrasi);

        [OperationContract]

        Produsen GetProdusen(string username);

        [OperationContract]
        IEnumerable<Produsen> GetListProdusen();

        [OperationContract]
        Produsen TambahProdusen(Produsen data);

        [OperationContract]
        Produsen GetProdusenById(string id);

        [OperationContract]
        Produsen_Vaksin TambahProdusenVaksin(Produsen_Vaksin data);

        [OperationContract]

        Produsen_Vaksin UpdateProdusenVaksin(Produsen_Vaksin data);

        [OperationContract]
        Feedback DeleteProdusenVaksin(string no_registrasi);

        [OperationContract]
        bool DeleteListProdusenVaksin(IEnumerable<Produsen_Vaksin> list);

        /* End Produsen Vaksin CRUD */


        /*
         * BPOM Service for RS
         * 
         * */

        //Vaksin RS
        [OperationContract]
        RumahSakit TambahRS(RumahSakit data);

        [OperationContract]
        RumahSakit GetRS(string username);

        [OperationContract]

        IEnumerable<RS_Vaksin> GetVaksinRS();

        [OperationContract]

        RS_Vaksin GetVaksinRSDetails(string no_registrasi);

        [OperationContract]

        RS_Vaksin TambahRSVaksin(RS_Vaksin data);

        [OperationContract]
        bool TambahListRSVaksin(IEnumerable<RS_Vaksin> list);

        [OperationContract]

        RS_Vaksin UpdateRSVaksin(RS_Vaksin data);

        [OperationContract]
        bool UpdateListRSVaksin(IEnumerable<RS_Vaksin> list);

        [OperationContract]
        Feedback DeleteRSVaksin(string no_registrasi);


        //End Vaksin RS

        [OperationContract]
        IEnumerable<RumahSakit> GetListRumahSakit();

        [OperationContract]
        RumahSakit GetDetailRumahSakit(string id);

        [OperationContract]

        IEnumerable<BPOM_Log_Kedatangan_Vaksin> GetLogArrivalVaccine();

        [OperationContract]

        BPOM_Log_Kedatangan_Vaksin GetLogArrivalVaccineDetails(string no_registrasi);

        [OperationContract]

        Feedback ReportArrivalVaccine(BPOM_Log_Kedatangan_Vaksin data);

        [OperationContract]
        bool ReportListArrivalVaccine(IEnumerable<BPOM_Log_Kedatangan_Vaksin> list);


        [OperationContract]
        IEnumerable<BPOM_Log_Penggunaan_Vaksin> GetLogVaccineUse();

        [OperationContract]
        BPOM_Log_Penggunaan_Vaksin GetLogVaccineUseDetails(string id);


        [OperationContract]
        Feedback ReportVaccineUse(BPOM_Log_Penggunaan_Vaksin data);

        [OperationContract]
        bool ReportListVaccineUse(IEnumerable<BPOM_Log_Penggunaan_Vaksin> list);

        [OperationContract]
        bool UpdateListVaccineUse(IEnumerable<BPOM_Log_Penggunaan_Vaksin> list);

        //End BPOM Service for RS


        //Masyarakat Service

        [OperationContract]
        Masyarakat GetMasyarakat(string username);

        [OperationContract]
        Masyarakat GetMasyarakatByNik(string nik);

        [OperationContract]
        IEnumerable<Masyarakat> GetListMasyarakat();

        [OperationContract]
        Masyarakat TambahMasyarakat(Masyarakat data);

        [OperationContract]
        bool UpdateMasyarakat(Masyarakat data);

        [OperationContract]
        bool DeleteMasyarakat(string nik);

        //End Masyarakat


        //Pasien Service

        [OperationContract]
        IEnumerable<Pasien> GetListPasien();

        [OperationContract]
        Pasien GetPasienByNoRek(string No_RekMedis);

        [OperationContract]
        Pasien TambahPasien(Pasien data);

        [OperationContract]
        bool EditPasien(Pasien data);

        [OperationContract]
        bool EditListPasien(IEnumerable<Pasien> list);

        [OperationContract]
        bool DeletePasien(string No_RekMedis);

        //End Pasien

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}

