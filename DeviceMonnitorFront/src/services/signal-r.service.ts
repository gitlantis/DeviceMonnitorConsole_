import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Constants } from 'src/constants';
import { DynamicData } from 'src/helpers/dynamic-data.model';
import { DataService } from './data.service';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: DynamicData[];
  private broadcastedData: DynamicData[];
  private dataService: DataService;
  private hubConnection: signalR.HubConnection;
  private constatnts: Constants;
  

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/Data')
    .build();

    this.hubConnection
    .start()
    .then(()=>console.log('connection started'))
    .catch(err=>console.log('Error while starting'))
  }

  public addTransferDataListener = () => {
    this.hubConnection.on('transferchartdata', (data)=>{      
      this.getDevices();
      console.log('transferdata'+this.data);
    });
  }

  public addBroadcastDataListener = () =>{
    this.hubConnection.on('BroadcastMessage', (data)=>{      
      this.getDevices();
      console.log("BR:"+this.data);
    });
  }

  getDevices() {        
    this.dataService.getDevices().subscribe(res => {
      this.data = res;
      console.log("sr:"+res);
    },
      err => {
        console.log(err);
      });
  }
  
}
