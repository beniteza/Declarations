import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NotifierModule } from 'angular-notifier';

import { AppComponent } from './app.component';
import { AccountService } from './services/account.service';
import { HomeService } from './services/home.service';
import { PetitionService } from './services/petition.service';
import { AppRoutingModule } from './app-routing.module';
import { AuthInterceptor } from './guards/auth.interceptor';
import { AboutComponent } from './components/home/about/about.component';
import { LoginComponent } from './components/home/login/login.component';
import { NotFoundComponent } from './components/home/not-found/not-found.component';
import { RegisterComponent } from './components/home/register/register.component';
import { PetitionComponent } from './components/petition/petition/petition.component';
import { AddUpdatePetitionComponent } from './components/petition/add-update-petition/add-update-petition.component';
import { AccountInfoComponent } from './components/account/account-info/account-info.component';
import { ChangePasswordComponent } from './components/account/change-password/change-password.component';
import { PetitionListComponent } from './components/petition/petition-list/petition-list.component';
import { MyPetitionListComponent } from './components/petition/my-petition-list/my-petition-list.component';
import { UnauthorizedComponent } from './components/home/unauthorized/unauthorized.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { PageLoadingComponent } from './components/shared/page-loading/page-loading.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxMaskModule, IConfig } from 'ngx-mask';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    LoginComponent,
    RegisterComponent,
    PetitionComponent,
    AddUpdatePetitionComponent,
    AccountInfoComponent,
    ChangePasswordComponent,
    PetitionListComponent,
    MyPetitionListComponent,
    NotFoundComponent,
    UnauthorizedComponent,
    NavbarComponent,
    PageLoadingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NotifierModule,
    NgxPaginationModule,
    NgxMaskModule.forRoot(maskConfig)
  ],
  providers: [
    HomeService,
    AccountService,
    PetitionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
