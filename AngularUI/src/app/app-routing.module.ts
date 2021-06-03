import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountInfoComponent } from './components/account/account-info/account-info.component';
import { ChangePasswordComponent } from './components/account/change-password/change-password.component';
import { AboutComponent } from './components/home/about/about.component';
import { LoginComponent } from './components/home/login/login.component';
import { NotFoundComponent } from './components/home/not-found/not-found.component';
import { RegisterComponent } from './components/home/register/register.component';
import { UnauthorizedComponent } from './components/home/unauthorized/unauthorized.component';
import { AddUpdatePetitionComponent } from './components/petition/add-update-petition/add-update-petition.component';
import { MyPetitionListComponent } from './components/petition/my-petition-list/my-petition-list.component';
import { PetitionListComponent } from './components/petition/petition-list/petition-list.component';
import { PetitionComponent } from './components/petition/petition/petition.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'about', component: AboutComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'unauthorized', component: UnauthorizedComponent},
  {path: 'change-password', component: ChangePasswordComponent, canActivate:[AuthGuard]},
  {path: 'account-info', component: AccountInfoComponent, canActivate:[AuthGuard]},
  {path: 'petition/list', component: PetitionListComponent},
  {path: 'petition/my-list', component: MyPetitionListComponent, canActivate:[AuthGuard]},
  {path: 'petition/:id', component: PetitionComponent},
  {path: 'petition/update/:id', component: AddUpdatePetitionComponent, canActivate:[AuthGuard]},
  {path: 'petition/add', component: AddUpdatePetitionComponent, canActivate:[AuthGuard]},
  {path: '**', redirectTo: '/not-found', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
