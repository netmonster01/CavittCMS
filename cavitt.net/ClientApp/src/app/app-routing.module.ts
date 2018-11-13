import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignUpComponent } from './login';
import { AdminComponent, LogsComponent } from './admin';
import { SignInComponent } from './login';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home';
import { PageNotFoundComponent } from './page-not-found';
import { StatsComponent } from './stats';
import { ProfileComponent } from './profile';
import { ForbiddenComponent } from './forbidden';
import { BlogsComponent } from './blogs';
import { AuthGuard, RoleGuard } from './guards';
import { ProjectsComponent} from './projects';

const appRoutes: Routes = [
 
  { path: 'home', component: HomeComponent },
  {
    path: 'admin', component: AdminComponent, canActivate: [AuthGuard, RoleGuard], data: { role: 'Admin' }},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'stats', component: StatsComponent },
  { path: 'login', component: SignInComponent },
  { path: 'register', component: SignUpComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'blog', component: BlogsComponent },
  { path: 'logs', component: LogsComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: '403', component: ForbiddenComponent },
  { path: '**', component: PageNotFoundComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
