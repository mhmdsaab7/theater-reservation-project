import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '@/_services';
import { AlertService } from '../_alert/alert.service';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  public username: string;
  public password: string;
  returnUrl: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/theater']);
    }
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/theater';
  }


  onSubmit() {
    this.alertService.clear();

    this.authenticationService.login(this.username, this.password)
      .pipe(first())
      .subscribe(
        data => {
          if (data != undefined) {
            this.router.navigate([this.returnUrl]);
          } else {
            this.alertService.error("Login failed! Wrong credentials.");

          }
        },
        error => {
          this.alertService.error(error);
        });
  }
}
