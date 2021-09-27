import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router, CanActivate, ActivatedRoute, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService, SeatAPIService } from '@/_services';
import { Seat, SeatDetail } from '@/_models';
import { AlertService } from '../_alert/alert.service';

@Component({
  selector: 'app-seat',
  templateUrl: './seat.component.html'
})
export class SeatComponent {
  public seatdetails: SeatDetail[];
  public theaterName: string;
  public theaterId: number;
  public playId: number;
  public seatNumber: string;
  public playName: string;
  public isAdmin: boolean = false;
  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    //public dialog: MatDialog,
    public activatedRoute: ActivatedRoute,
    private alertService: AlertService,
    private seatAPIService: SeatAPIService,
    private authenticationService: AuthenticationService) {
    this.isAdmin = this.authenticationService.currentUserValue.isAdmin;

    var params = activatedRoute.snapshot.queryParams;
    this.theaterId = params.theaterId;
    this.theaterName = params.theaterName;
    this.playId = params.playId;
    this.playName = params.playName;
    this.GetFilteredSeats();
  }

  public search() {
    return this.GetFilteredSeats();
  }

  public add() {
    //const dialogRef = this.dialog.open(SeatEditor, {
    //  width: '250px',
    //  disableClose: true
    //});

    //dialogRef.afterClosed().subscribe(result => {
    //  return this.GetFilteredSeats();
    //});
  }
  public showReserveAction(seatDetail: SeatDetail) {
    return seatDetail.showReserveAction && !this.isAdmin;
  }
  public showConfirmAction(seatDetail: SeatDetail) {
    return seatDetail.showConfirmAction && this.isAdmin;
  }
  public showDeclineAction(seatDetail: SeatDetail) {
    return seatDetail.showDeclineAction && this.isAdmin;
  }

  public getClass(seatDetail: SeatDetail) {
    switch (seatDetail.seatStatusName) {
      case "Declined": return "status-red";
      case "Confirmed": return "status-green";
      case "Requested": return "status-orange";
      case "Already Taken": return "status-gray";
      default: return "";
    }
  }
  private GetFilteredSeats() {
    return this.seatAPIService.GetFilteredSeats(this.theaterId, this.playId, this.seatNumber).subscribe((result) => {
      this.seatdetails = result;
    }, error => console.error(error));

  }

  public delete(seat: SeatDetail) {
    return this.seatAPIService.DeleteSeat(seat.seatPlayId).subscribe(result => {
      if (result.isSucceeded) {
        this.GetFilteredSeats();
      } else {
        this.alertService.error(result.errorMessage);

      }
    });
  }

  public confirm(seat: SeatDetail) {
    return this.seatAPIService.ConfirmSeat(seat.seatPlayId).subscribe(result => {
      if (result.isSucceeded) {
        this.GetFilteredSeats();
      } else {
        this.alertService.error(result.errorMessage);

      }
    });
  }

  public reserve(seat: SeatDetail) {
    return this.seatAPIService.ReserveSeat(seat.seatId, this.playId).subscribe(result => {
      if (result && result.isSucceeded) {
        this.GetFilteredSeats();
      } else {
        this.alertService.error(result.errorMessage);

      }
    });
  }

  public decline(seat: SeatDetail) {
    return this.seatAPIService.DeclineSeat(seat.seatPlayId).subscribe(result => {
      if (result.isSucceeded) {
        this.GetFilteredSeats();
      } else {
        this.alertService.error(result.errorMessage);

      }
    });
  }

  public edit(seat: SeatDetail) {
    //const dialogRef = this.dialog.open(SeatEditor, {
    //  width: '250px',
    //  disableClose: true,
    //  data: { seatId: seat.SeatId, name: seat.SeatNumber }
    //});

    //dialogRef.afterClosed().subscribe(result => {
    //  // update seat
    //  seat.SeatNumber = result;
    //  this.seatAPIService.UpdateSeat(seat);
    //});
  }

}
//@Component({
//  selector: 'seat-editor',
//  templateUrl: 'seat-editor.html',
//})
//export class SeatEditor {
//  dialogTitle: string;
//  constructor(
//    public dialogRef: MatDialogRef<SeatEditor>,
//    @Inject(MAT_DIALOG_DATA) public data: SeatDialogData) {
//    this.dialogTitle = data != undefined ? "Add New Seat" : "Edit Seat: " + data.number;
//  }

//  onNoClick(): void {
//    this.dialogRef.close();
//  }

//}


//export interface SeatDialogData {
//  seatId: string;
//  number: string;
//}
