import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginScreenComponent } from './login-screen/login-screen.component';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from '../services/auth.guard';
import { AppRoutingModule } from './app-routing.module';
import { Constants } from 'src/constants';
import { ModalContentComponent } from './modal-content/modal-content.component';
import { CommonModule, registerLocaleData } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ModalArchiveComponent } from './modal-archive/modal-archive.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import en from '@angular/common/locales/en';
import { NgZorroAntdModule } from './ng-zorro-ant.module';


registerLocaleData(en);
@NgModule({
  declarations: [
    AppComponent,
    LoginScreenComponent,
    MainScreenComponent,    
    ModalContentComponent, 
    ModalArchiveComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added 
    NgZorroAntdModule
  ],
  providers: [AuthGuard, Constants, { provide: NZ_I18N, useValue: en_US }],
  bootstrap: [AppComponent],
  entryComponents: [ ModalContentComponent ]
})
export class AppModule { }
