import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TheaterComponent } from './theater/theater.component';
import { AuthenticationService, PlayAPIService, SeatAPIService, TheaterAPIService, UserAPIService } from './_services';
import BaseAPIService from './_services/baseAPI.service';
import { PlayComponent } from './play/play.component';
import { LoginComponent } from './login/login.component';
import { SeatComponent } from './seat/seat.component';
import { AuthGuard } from './_helpers';
import { AlertComponent } from './_alert/alert.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TheaterComponent,
    PlayComponent,
    LoginComponent,
    SeatComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full', },
      { path: 'theater', component: TheaterComponent, canActivate: [AuthGuard] },
      { path: 'play', component: PlayComponent, canActivate: [AuthGuard] },
      { path: 'seat', component: SeatComponent, canActivate: [AuthGuard] },
    ])
  ],
  providers: [TheaterAPIService, BaseAPIService, PlayAPIService, SeatAPIService, UserAPIService, AuthenticationService],
  bootstrap: [AppComponent],
})
export class AppModule { }
