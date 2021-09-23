using DeviceMonnitorAPI.DBModels;
using DeviceMonnitorAPI.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Services
{
    public class DeviceService
    {
        private readonly MyDBContext _myDbContext;
        private readonly IFirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = Constants.FirebaseSecret,
            BasePath = Constants.FirebaseUrl
        };
        private IFirebaseClient client;
        public DeviceService(MyDBContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<Device> AddDevice(Device model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.EditedDate = DateTime.Now;

                _myDbContext.AddAsync(model);
                await _myDbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Guid?> EditDevice(DeviceModel model)
        {
            try
            {
                var dev = _myDbContext.Device.Where(c => c.DeviceGuid == model.DeviceGuid).FirstOrDefault();

                dev.Name = model.Name;
                dev.Description = model.Description;
                dev.IsActive = model.IsActive;
                dev.CreatedDate = dev.CreatedDate;
                dev.EditedDate = DateTime.Now;

                await _myDbContext.SaveChangesAsync();
                return model.DeviceGuid;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Guid?> DeleteDevice(Guid guid)
        {
            try
            {
                var dev = _myDbContext.Device.Where(c => c.DeviceGuid == guid).FirstOrDefault();

                _myDbContext.Device.Remove(dev);
                await _myDbContext.SaveChangesAsync();

                return dev.DeviceGuid;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<DeviceModel> GetDeviceByGuid(Guid guid)
        {
            try
            {
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var result = _myDbContext.Device.Where(c => c.DeviceGuid == guid).Select(c =>
                new DeviceModel
                {
                    DeviceGuid = c.DeviceGuid,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    IsActive = c.IsActive,
                    EditedDate = c.EditedDate
                }).FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<DeviceModel>> GetDevices()
        {
            try
            {
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var result = _myDbContext.Device.Select(c =>
                new DeviceModel
                {
                    DeviceGuid = c.DeviceGuid,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    IsActive = c.IsActive,
                    EditedDate = c.EditedDate
                }
                );
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<DeviceUsersModel> GetDeviceUsers(Guid guid)
        {
            try
            {
                var res = await _myDbContext.Device.Include(c => c.Users).Where(c => c.DeviceGuid == guid).FirstOrDefaultAsync();

                var result = new DeviceUsersModel
                {
                    UserGuid = res.Users.Select(c => c.UserGuid).ToArray(),
                    DeviceGuid = guid
                };

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DeviceUsersModel> ConnectUsers(DeviceUsersModel model)
        {
            try
            {
                var users = _myDbContext.Users.Where(c => model.UserGuid.Contains(c.UserGuid)).ToList();
                var dev = await _myDbContext.Device.Include(u => u.Users).Where(c => c.DeviceGuid == model.DeviceGuid).FirstOrDefaultAsync();
                //var dev = _myDbContext.Device.Include(u=>u.Users).Where(c => c.DeviceGuid == model.DeviceGuid).FirstOrDefault();
                var userCheck = dev.Users.DefaultIfEmpty(null);
                if (userCheck.FirstOrDefault() != null)
                {
                    dev.Users.Clear();
                }
                var result = dev.Users = users;
                await _myDbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<DeviceConfigModel> SetConfig(DeviceConfigModel model)
        {
            try
            {
                var fbArr = new dynamic[9];
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var dbModel = new DeviceConfig();

                dbModel.DeviceGuid = model.DeviceGuid;
                fbArr[0] = dbModel.UMax = model.UMax;
                fbArr[1] = dbModel.UMin = model.UMin;
                //fbArr[2] = dbModel.Cup = 1;
                fbArr[2] = dbModel.Calm = model.Calm;
                //fbArr[4] = dbModel.Cadw = 1;
                fbArr[3] = dbModel.Wup = model.Wup;
                fbArr[4] = dbModel.Wdw = model.Wdw;
                //fbArr[7] = dbModel.Ontime = 1;
                //fbArr[8] = dbModel.Ertime = 1;
                fbArr[5] = dbModel.Overtime = model.Overtime;
                fbArr[6] = dbModel.DownTime = model.DownTime;
                fbArr[7] = dbModel.OverVtime = model.OverVtime;
                fbArr[8] = dbModel.LowVtime = model.LowVtime;
                dbModel.DO0 = model.DO0;
                dbModel.DO1 = model.DO1;
                dbModel.DO2 = model.DO2;
                dbModel.DO3 = model.DO3;
                //fbArr[13] = dbModel.EMode = 1;

                dbModel.ConfGuid = Guid.NewGuid();
                dbModel.CreatedDate = DateTime.Now;

                _myDbContext.Add(dbModel);
                _myDbContext.SaveChanges();

                model.CreatedDate = dbModel.CreatedDate;
                model.ConfGuid = dbModel.ConfGuid;

                //write to firebase
                client = new FirebaseClient(fbc);
                //var resp = await client.SetAsync("devSetting/" + model.DeviceGuid.ToString(), fbArr);

                return model;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }

        public async Task<DeviceConfigModel> GetConfigByDeviceGuid(Guid guid)
        {
            try
            {
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var result = _myDbContext.DeviceConfig.Where(c => c.DeviceGuid == guid).OrderByDescending(t => t.CreatedDate).
                    Select(c => new DeviceConfigModel
                    {
                        ConfGuid = c.ConfGuid,
                        DeviceGuid = c.DeviceGuid,
                        UMax = c.UMax,
                        UMin = c.UMin,
                        //Cup = c.Cup,
                        Calm = c.Calm,
                        //Cadw = c.Cadw,
                        Wup = c.Wup,
                        Wdw = c.Wdw,
                        //Ontime = c.Ontime,
                        //Ertime = c.Ertime,
                        Overtime = c.Overtime,
                        DownTime = c.DownTime,
                        OverVtime = c.OverVtime,
                        LowVtime = c.LowVtime,
                        //EMode = c.EMode,
                        CreatedDate = c.CreatedDate,
                        EditedDate = c.EditedDate,
                        DO0 = c.DO0,
                        DO1 = c.DO1,
                        DO2 = c.DO2,
                        DO3 = c.DO3,
                    }).FirstOrDefaultAsync();

                var res = await result;
                return res;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public async Task<Dictionary<Guid, DeviceConfigAsMassModel>> GetConfigAsMass(Guid guid)
        {
            try
            {
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var result = await _myDbContext.DeviceConfig.Where(c => c.DeviceGuid == guid).OrderByDescending(t => t.CreatedDate).FirstOrDefaultAsync();

                var res = new Dictionary<Guid, DeviceConfigAsMassModel>();
                var config = new DeviceConfigAsMassModel();

                config.AI = new decimal[9];
                config.DO = new bool[4];

                config.AI[0] = result.UMax;
                config.AI[1] = result.UMin;
                config.AI[2] = result.Calm;
                config.AI[3] = result.Wup;
                config.AI[4] = result.Wdw;
                config.AI[5] = result.Overtime;
                config.AI[6] = result.DownTime;
                config.AI[7] = result.OverVtime;
                config.AI[8] = result.LowVtime;
                
                config.DO[0] = result.DO0;
                config.DO[1] = result.DO1;
                config.DO[2] = result.DO2;
                config.DO[3] = result.DO3;

                res.Add(guid, config);

                return res;
            }
            catch (Exception e)
            {
                return null;

            }
        }

    }
}
