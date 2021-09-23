import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { DeviceConfig } from 'src/helpers/device-config.model';
import { DeviceData } from 'src/helpers/device-data.model';
import { DataService } from 'src/services/data.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modal-content',
  templateUrl: './modal-content.component.html',
  styleUrls: ['./modal-content.component.css']
})
export class ModalContentComponent implements OnInit {
  @Input() public guid;
  @Input() public deviceName;

  deviceConfig: DeviceConfig;
  colNames: string[];
  pageSize: number;
  bDate: string;
  eDate: string;

  constructor(private modalService: NgbModal, private dataService: DataService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getConfig(this.guid)
  }

  getConfig(guid) {
    this.dataService.getConfig(guid)
      .subscribe(
        res => {
          this.deviceConfig = res;
        },
        err => {
          this.toastr.error(err.message, 'Config read error!');
          //console.log(err)
        })
  }

  closeModal() {
    const modalRef = this.modalService.dismissAll(ModalContentComponent);
  }

  onSubmit(form: NgForm) {

    form.value.deviceGuid = this.guid
    
    if (form.value.dO0.length == 0)
      form.value.dO0 = this.deviceConfig.dO0;
    if (form.value.dO1.length == 0)
      form.value.dO1 = this.deviceConfig.dO1;
    if (form.value.dO2.length == 0)
      form.value.dO2 = this.deviceConfig.dO2;
    if (form.value.dO3.length == 0)
      form.value.dO3 = this.deviceConfig.dO3;

    this.dataService.postConfig(form.value)
  }

}
