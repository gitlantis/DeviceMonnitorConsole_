import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/constants';
import { Observable } from 'rxjs';
import { DynamicData } from 'src/helpers/dynamic-data.model';
import { DeviceConfig } from 'src/helpers/device-config.model';
import { BaseService } from './base.service';
import { UserService } from './user.service';
import { ToastrService } from 'ngx-toastr';
import { ArchiveData } from 'src/helpers/archive-data.model';
@Injectable({
  providedIn: 'root'
})
export class DataService extends BaseService {

  resp: null;

  constructor(httpClient: HttpClient, router: Router, constants: Constants, userService: UserService, private toastr: ToastrService) {
    super(httpClient, router, constants, userService)
  }


  getDevices(): Observable<DynamicData[]> {
    return this.httpClient.post<DynamicData[]>(this.constants.baseUrl + '/DeviceData/GetDynamicData', "", { headers: this.header }) as Observable<DynamicData[]>
  }

  getConfig(guid): Observable<DeviceConfig> {
    return this.httpClient.post<DeviceConfig>(this.constants.baseUrl + '/Device/GetConfig', JSON.stringify(guid), { headers: this.header })
  }

  postConfig(formData) {
    return this.httpClient.post(this.constants.baseUrl + '/Device/AddConfig', this.toRawDevConfValue(formData), { headers: this.header }).subscribe(
      res => {
        this.toastr.success('Configuration saved!', 'Configuration!');
      },
      err => {
        this.toastr.error('Check configuration ranges!', 'Data error!');
      });
  }

  getArchive(data): Observable<Array<ArchiveData>> {
    return this.httpClient.post<Array<ArchiveData>>(this.constants.baseUrl + '/DeviceData/GetArchive', JSON.stringify(data), { headers: this.header })
  }

  toRawDevConfValue<T extends DeviceConfig>(formData: T): DeviceConfig {
    return {
      confGuid: "",
      deviceGuid: formData.deviceGuid,
      uMax: formData.uMax,
      uMin: formData.uMin,
      //cup: formData.cup,
      calm: formData.calm,
      //cadw: formData.cadw,
      wup: formData.wup,
      wdw: formData.wdw,
      //ontime: formData.ontime,
      //ertime: formData.ertime,
      overtime: formData.overtime,
      downTime: formData.downTime,
      overVtime: formData.overVtime,
      lowVtime: formData.lowVtime,
      //eMode: formData.eMode,
      createdDate: null,
      editedDate: null,
      dO0: formData.dO0,
      dO1: formData.dO1,
      dO2: formData.dO2,
      dO3: formData.dO3
    }
  }

  toRawArchValue<T extends ArchiveData>(requestData: ArchiveData): ArchiveData {
    return {
      deviceGuid: requestData.deviceGuid,
      name: requestData.name,
      dataCount: requestData.dataCount,
      itemCount: requestData.itemCount,
      pageNum: requestData.pageNum,
      rowCount: requestData.rowCount,
      createdDate: requestData.createdDate,
      ai: requestData.ai,
      ao: requestData.ao,
      di: requestData.di,
      do: requestData.do,
      metadata: requestData.metadata,
      pageCount: requestData.pageCount
    }
  }

}



