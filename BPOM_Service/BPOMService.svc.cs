using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using TrackingVaksinService.Helper;

namespace TrackingVaksinService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BPOMService : IEBPOMService
    {
        // Autentication
        public Akun Register(Akun data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            try
            {
                if (data == null)
                    return null;
                if (GetAkun(data.username) != null)
                    return null;
                trackingvaksinDB.Akun.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       public bool Login(string username, string password)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Akun getAkun = trackingvaksinDB.Akun.FirstOrDefault(X => X.username.Equals(username) && X.password.Equals(password));
            if (getAkun != null)
            {
                trackingvaksinDB.Dispose();
                return true;
            }
            return false;
        }

        public Akun GetAkun(string username)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Akun getAkun = trackingvaksinDB.Akun.FirstOrDefault(X => X.username.Equals(username));
            return getAkun;
        }
        //End Autentication

        //BPOM Service

        public BPOM GetBPOM(string username)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            BPOM getData = trackingvaksinDB.BPOM.FirstOrDefault(X => X.username.Equals(username));
            if (getData != null)
                return getData;
            return null;
        }

        public IEnumerable<BPOM_Vaksin> GetVaksin()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<BPOM_Vaksin> listVaksin = trackingvaksinDB.BPOM_Vaksin.ToList();

            return listVaksin;
        }

        public BPOM_Vaksin GetVaksinDetails(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            BPOM_Vaksin vaksinbyNoReg = trackingvaksinDB.BPOM_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            return vaksinbyNoReg;
        }

        public BPOM_Vaksin Up(BPOM_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            data.create_at = DateTime.Now;


            if (data != null)
            {
                if (trackingvaksinDB.BPOM_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi) != null)
                {
                    return null;
                }
                trackingvaksinDB.BPOM_Vaksin.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            else
            {
                trackingvaksinDB.Dispose();
                return null;
            }
        }

        public BPOM_Vaksin UpdateVaksin(BPOM_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                BPOM_Vaksin curData = trackingvaksinDB.BPOM_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi);
                data.create_at = DateTime.Now;
                if (curData != null)
                {
                    curData.status = data.status;
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                    return curData;
                }
                else
                    throw new Exception("No Registrasi not found");
            }
            return null;
        }

        public Feedback DeleteVaksin(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            BPOM_Vaksin curData = trackingvaksinDB.BPOM_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            if (curData != null)
            {
                trackingvaksinDB.BPOM_Vaksin.Remove(curData);
                trackingvaksinDB.SaveChanges();
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Success Delete Data"
                };
            }
            else
            {
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Failed to Delete Data",
                    Message = "ID Not Available on Record"
                };
            }
        }


        // Produsen

        public IEnumerable<Produsen> GetListProdusen()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<Produsen> list = trackingvaksinDB.Produsen.ToList();
            return list;
        }

        public Produsen GetProdusen(string username)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Produsen getData = trackingvaksinDB.Produsen.FirstOrDefault(X => X.username.Equals(username));
            if (getData != null)
                return getData;
            return null;
        }

        public Produsen TambahProdusen(Produsen data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                trackingvaksinDB.Produsen.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            return null;
        }

        public Produsen GetProdusenById(string id)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            int id_p = int.Parse(id);
            Produsen getData = trackingvaksinDB.Produsen.FirstOrDefault(X => X.id == id_p);
            if (getData != null)
                return getData;
            return null;
        }

        public IEnumerable<Produsen_Vaksin> GetProdusenVaksin(string id_produsen)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            int id = int.Parse(id_produsen);
            IEnumerable<Produsen_Vaksin> list = trackingvaksinDB.Produsen_Vaksin.Where(X => (X.id_produsen == id)).ToList();
            return list;
        }

        public Produsen_Vaksin GetProdusenVaksinDetails(string id_produsen, string no_registrasi)
        {
            IEnumerable<Produsen_Vaksin> list = GetProdusenVaksin(id_produsen);
            Produsen_Vaksin databyNoReg = list.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            return databyNoReg;
        }

        public Produsen_Vaksin GetProdusenVaksinByNoReg(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Produsen_Vaksin databyNoReg = trackingvaksinDB.Produsen_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            return databyNoReg;
        }

        public Produsen_Vaksin TambahProdusenVaksin(Produsen_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            data.create_at = DateTime.Now;


            if (data != null)
            {
                if (trackingvaksinDB.Produsen_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi) != null)
                {
                    return null;
                }
                trackingvaksinDB.Produsen_Vaksin.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            else
            {
                trackingvaksinDB.Dispose();
                return null;
            }
        }

        public Produsen_Vaksin UpdateProdusenVaksin(Produsen_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                Produsen_Vaksin curData = trackingvaksinDB.Produsen_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi);
                data.create_at = DateTime.Now;
                if (curData != null)
                {
                    curData.status = data.status;
                    curData.kemasan = data.kemasan;
                    curData.jumlah = data.jumlah;
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                    return curData;
                }
                else
                    throw new Exception("No Registrasi not found");
            }
            return null;
        }

        public Feedback DeleteProdusenVaksin(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Produsen_Vaksin curData = trackingvaksinDB.Produsen_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            if (curData != null)
            {
                trackingvaksinDB.Produsen_Vaksin.Remove(curData);
                trackingvaksinDB.SaveChanges();
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Success Delete Data"
                };
            }
            else
            {
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Failed to Delete Data",
                    Message = "ID Not Available on Record"
                };
            }
        }

        public bool DeleteListProdusenVaksin(IEnumerable<Produsen_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            try
            {
                trackingvaksinDB.Produsen_Vaksin.RemoveRange(list);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            catch
            {

                return false;
            }
        }

        // ENd Produsen Vaksin


        // Rumah Sakit

        public RumahSakit GetRS(string username)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            RumahSakit getData = trackingvaksinDB.RumahSakit.FirstOrDefault(X => X.username.Equals(username));
            if (getData != null)
                return getData;
            return null;
        }

        public RumahSakit TambahRS(RumahSakit data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                trackingvaksinDB.RumahSakit.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            return null;
        }

        public bool UpdateListRSVaksin(IEnumerable<RS_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (list != null)
            {
                foreach (RS_Vaksin data in list)
                {
                    RS_Vaksin newvaksin = trackingvaksinDB.RS_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi);
                    newvaksin.id_produsen = data.id_produsen;
                    newvaksin.kode_ref = data.kode_ref;
                    newvaksin.isReported = data.isReported;
                    newvaksin.jumlah = data.jumlah;
                    newvaksin.kemasan = data.kemasan;
                    trackingvaksinDB.SaveChanges();
                }
                return true;
            }
            else
                return false;
        }

        public bool EditListPasien(IEnumerable<Pasien> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (list != null)
            {
                foreach (Pasien data in list)
                {
                    Pasien newPasien = trackingvaksinDB.Pasien.FirstOrDefault(X => X.no_registrasi == data.no_registrasi);
                    newPasien.no_registrasi = data.no_registrasi;
                    newPasien.id_RumahSakit = data.id_RumahSakit;
                    newPasien.NIK = data.NIK;
                    newPasien.isReported = data.isReported;
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                    return true;
                }
                return true;
            }
            else
                return false;
        }

        public IEnumerable<BPOM_Log_Kedatangan_Vaksin> GetLogArrivalVaccine()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<BPOM_Log_Kedatangan_Vaksin> list = trackingvaksinDB.BPOM_Log_Kedatangan_Vaksin.ToList();
            return list;
        }

        public BPOM_Log_Kedatangan_Vaksin GetLogArrivalVaccineDetails(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            BPOM_Log_Kedatangan_Vaksin logbynoReq = trackingvaksinDB.BPOM_Log_Kedatangan_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            return logbynoReq;
        }

        public Feedback ReportArrivalVaccine(BPOM_Log_Kedatangan_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            data.create_at = DateTime.Now;
            Feedback feed = new Feedback();
            // data != null
            if (data != null)
            {
                // vaksin is available in BPOM (Valid) or Not
                if (trackingvaksinDB.BPOM_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi) != null)
                {
                    //vaksin available

                    // no_registrasi duplicate
                    if (trackingvaksinDB.BPOM_Log_Kedatangan_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi) != null)
                    {
                        trackingvaksinDB.Dispose();
                        feed.statusCode = HttpStatusCode.BadRequest.ToString();
                        feed.Message = "Faild to record Report";
                        feed.Description = "Dupplicate Registration Number";
                        return feed;
                    }
                    trackingvaksinDB.BPOM_Log_Kedatangan_Vaksin.Add(data);
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                    feed.statusCode = HttpStatusCode.OK.ToString();
                    feed.Message = "Report was recorded";
                    return feed;

                }
                else
                {
                    trackingvaksinDB.Dispose();
                    feed.statusCode = HttpStatusCode.BadRequest.ToString();
                    feed.Message = "Faild to record Report";
                    feed.Description = "Vaccine was not valid , no_registrasi not found in BPOM Record";
                    return feed;
                }
            }
            else
            {
                trackingvaksinDB.Dispose();
                return null;
            }
        }

        public bool ReportListArrivalVaccine(IEnumerable<BPOM_Log_Kedatangan_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if(list.Count() != 0)
            {
                trackingvaksinDB.BPOM_Log_Kedatangan_Vaksin.AddRange(list);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            return false;
        }

        public IEnumerable<BPOM_Log_Penggunaan_Vaksin> GetLogVaccineUse()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<BPOM_Log_Penggunaan_Vaksin> list = trackingvaksinDB.BPOM_Log_Penggunaan_Vaksin.ToList();
            return list;
        }

        public BPOM_Log_Penggunaan_Vaksin GetLogVaccineUseDetails(string id)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            int idparse = int.Parse(id);
            BPOM_Log_Penggunaan_Vaksin data = trackingvaksinDB.BPOM_Log_Penggunaan_Vaksin.FirstOrDefault(X => X.id == idparse);
            return data;
        }


        public Feedback ReportVaccineUse(BPOM_Log_Penggunaan_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            data.create_at = DateTime.Now;
            Feedback feed = new Feedback();
            if (trackingvaksinDB.Masyarakat.FirstOrDefault(X => X.NIK == data.NIK) != null)
            {
                trackingvaksinDB.BPOM_Log_Penggunaan_Vaksin.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                feed.statusCode = HttpStatusCode.Accepted.ToString();
                feed.Message = "Report was recoreded";
                return feed;
            }
            else
            {
                feed.statusCode = HttpStatusCode.BadRequest.ToString();
                feed.Message = "Failed to Record Report";
                feed.Description = "NIK not registered in Pemerintah's Store";
                trackingvaksinDB.Dispose();
                return feed;
            }
        }

        public bool ReportListVaccineUse(IEnumerable<BPOM_Log_Penggunaan_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (list != null)
            {
                trackingvaksinDB.BPOM_Log_Penggunaan_Vaksin.AddRange(list);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            return false;
        }

        public bool UpdateListVaccineUse(IEnumerable<BPOM_Log_Penggunaan_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (list != null)
            {
                foreach (BPOM_Log_Penggunaan_Vaksin data in list)
                {
                    BPOM_Log_Penggunaan_Vaksin newvaksin = trackingvaksinDB.BPOM_Log_Penggunaan_Vaksin.FirstOrDefault(X => X.No_RekMedis == data.No_RekMedis);
                    newvaksin.NIK = data.NIK;
                    newvaksin.No_RekMedis = data.No_RekMedis;
                    newvaksin.no_registrasi = data.no_registrasi;
                    newvaksin.id_RumahSakit = data.id_RumahSakit;
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                }
                return true;
            }
            else
                return false;
        }

        //End

        /*
         * Implement Service Rumah Sakit
         * 
         */

        public IEnumerable<RumahSakit> GetListRumahSakit()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<RumahSakit> listRS = trackingvaksinDB.RumahSakit.ToList();

            return listRS;
        }

        public RumahSakit GetDetailRumahSakit(string id)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            int idRs = int.Parse(id);
            RumahSakit getById = trackingvaksinDB.RumahSakit.FirstOrDefault(X => X.id == idRs);
            return getById;
        }

        /*
         * 
         * RS VAKSIN
         * 
         * 
         */
        public IEnumerable<RS_Vaksin> GetVaksinRS()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<RS_Vaksin> listRS = trackingvaksinDB.RS_Vaksin.ToList();
            return listRS;
        }

        public RS_Vaksin GetVaksinRSDetails(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            RS_Vaksin detail = trackingvaksinDB.RS_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            return detail;
        }

        public RS_Vaksin TambahRSVaksin(RS_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if(data != null)
            {
                trackingvaksinDB.RS_Vaksin.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            return null;
        }

        public bool TambahListRSVaksin(IEnumerable<RS_Vaksin> list)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            try
            {
                trackingvaksinDB.RS_Vaksin.AddRange(list);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            catch
            {

                return false;
            }
        }


        public RS_Vaksin UpdateRSVaksin(RS_Vaksin data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                RS_Vaksin curData = trackingvaksinDB.RS_Vaksin.FirstOrDefault(X => X.no_registrasi == data.no_registrasi);
                data.create_at = DateTime.Now;
                if (curData != null)
                {
                    curData.kode_ref = data.kode_ref;
                    curData.kemasan = data.kemasan;
                    curData.jumlah = data.jumlah;
                    curData.isReported = data.isReported;
                    trackingvaksinDB.SaveChanges();
                    trackingvaksinDB.Dispose();
                    return curData;
                }
                else
                    throw new Exception("No Registrasi not found");
            }
            return null;
        }

        public Feedback DeleteRSVaksin(string no_registrasi)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            RS_Vaksin curData = trackingvaksinDB.RS_Vaksin.FirstOrDefault(X => X.no_registrasi == no_registrasi);
            if (curData != null)
            {
                trackingvaksinDB.RS_Vaksin.Remove(curData);
                trackingvaksinDB.SaveChanges();
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Success Delete Data"
                };
            }
            else
            {
                return new Feedback
                {
                    statusCode = HttpStatusCode.OK.ToString(),
                    Description = "Failed to Delete Data",
                    Message = "ID Not Available on Record"
                };
            }
        }

        //End RS Vaksin

        //Masyarakat

        public IEnumerable<Masyarakat> GetListMasyarakat()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<Masyarakat> list = trackingvaksinDB.Masyarakat.ToList();
            return list;
        }
        public Masyarakat GetMasyarakat(string username)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Masyarakat getData = trackingvaksinDB.Masyarakat.FirstOrDefault(X => X.username.Equals(username));
            if (getData != null)
                return getData;
            return null;
        }

        public Masyarakat GetMasyarakatByNik(string nik)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Masyarakat getData = trackingvaksinDB.Masyarakat.FirstOrDefault(X => X.NIK.Equals(nik));
            if (getData != null)
                return getData;
            return null;
        }

        public Masyarakat TambahMasyarakat(Masyarakat data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                trackingvaksinDB.Masyarakat.Add(data);
                trackingvaksinDB.SaveChanges();
            }
            trackingvaksinDB.Dispose();
            return data;
        }

        public bool UpdateMasyarakat(Masyarakat data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Masyarakat getCurr = trackingvaksinDB.Masyarakat.FirstOrDefault(X => X.NIK.Equals(data.NIK));
            if (getCurr != null)
            {
                getCurr.Nama = data.Nama;
                getCurr.JK = data.JK;
                getCurr.TglLahir = data.TglLahir;
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            trackingvaksinDB.Dispose();
            return false;
            
        }

        public bool DeleteMasyarakat(string NIK)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Masyarakat getCurr = trackingvaksinDB.Masyarakat.FirstOrDefault(X => X.NIK.Equals(NIK));
            if (getCurr != null)
            {
                trackingvaksinDB.Masyarakat.Remove(getCurr);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            trackingvaksinDB.Dispose();
            return false;
        }


        //End Masyarakat

        //Pasien Service
        public IEnumerable<Pasien> GetListPasien()
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            IEnumerable<Pasien> list = trackingvaksinDB.Pasien.ToList();
            return list;
        }

        public Pasien GetPasienByNoRek(string No_RekMedis)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Pasien byNorek = trackingvaksinDB.Pasien.FirstOrDefault(X => X.No_RekMedis.Equals(No_RekMedis));
            trackingvaksinDB.Dispose();
            return byNorek;
        }

        public Pasien TambahPasien(Pasien data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            if (data != null)
            {
                trackingvaksinDB.Pasien.Add(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return data;
            }
            return null;
        }

        public bool EditPasien(Pasien data)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Pasien newData = trackingvaksinDB.Pasien.FirstOrDefault(X => X.No_RekMedis.Equals(data.No_RekMedis));
            
            if(newData!= null)
            {
                newData.no_registrasi = data.no_registrasi;
                newData.id_RumahSakit = data.id_RumahSakit;
                newData.NIK = data.NIK;
                newData.isReported = data.isReported;
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;
            }
            trackingvaksinDB.Dispose();
            return false;
        }

        public bool DeletePasien(string No_RekMedis)
        {
            TrackingVaksinEntities trackingvaksinDB = new TrackingVaksinEntities();
            Pasien data = trackingvaksinDB.Pasien.FirstOrDefault(X => X.No_RekMedis.Equals(No_RekMedis));
            if (data != null)
            {
                trackingvaksinDB.Pasien.Remove(data);
                trackingvaksinDB.SaveChanges();
                trackingvaksinDB.Dispose();
                return true;    
            }
            trackingvaksinDB.Dispose();
            return false;
        }
        //End Pasien Service
    }
}
