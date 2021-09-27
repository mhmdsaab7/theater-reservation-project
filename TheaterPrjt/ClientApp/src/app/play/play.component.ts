import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router, CanActivate, ActivatedRoute, RouterStateSnapshot } from '@angular/router';
import {  AuthenticationService, PlayAPIService } from '@/_services';
import { Play } from '@/_models';
import { TheaterComponent } from '../theater/theater.component';
import { AlertService } from '../_alert/alert.service';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  providers: [TheaterComponent]
})



export class PlayComponent {
  public plays: Play[];
  public theaterName: string;
  theaterId: number;
  public playName: string;
  public effectiveTime: Date;
  public isAdmin: boolean = false;
  constructor(
    //public dialog: MatDialog,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private alertService: AlertService,
    private playAPIService: PlayAPIService,
    private authenticationService: AuthenticationService) {
    var params = activatedRoute.snapshot.queryParams;
    this.theaterId = params.theaterId;
    this.theaterName = params.theaterName;
    this.isAdmin = this.authenticationService.currentUserValue.isAdmin;
    this.GetFilteredPlays();
  }


  public search() {
    return this.GetFilteredPlays();
  }

  public add() {
    //const dialogRef = this.dialog.open(PlayEditor, {
    //  width: '250px',
    //  disableClose: true
    //});

    //dialogRef.afterClosed().subscribe(result => {
    //let playToAdd = {
    //  Name: "",
    //  StartDate: Date.now(),
    //  EndDate: Date.now(),
    //  TheaterId: this.theaterId
    //};
    //return this.playAPIService.AddPlay(playToAdd);
    //return this.GetFilteredPlays();
    //});
  }

  private GetFilteredPlays() {
    return this.playAPIService.GetFilteredPlays(this.theaterId, this.playName, this.effectiveTime).subscribe((result) => {
      this.plays = result;
    }, error => console.error(error));

  }

  public view(play: Play) {
    this.router.navigate(['/seat'], { queryParams: { theaterId: this.theaterId, theaterName: this.theaterName, playId: play.playId, playName: play.playName } });
  }
  public delete(play: Play) {
    return this.playAPIService.DeletePlay(play.playId).subscribe(result => {
      if (result.isSucceeded) {
        return this.GetFilteredPlays();
      } else {
        this.alertService.error(result.errorMessage);
      }
    });
  }

  public edit(play: Play) {
    //const dialogRef = this.dialog.open(PlayEditor, {
    //  width: '250px',
    //  disableClose: true,
    //  data: { playId: play.PlayId, name: play.Name, startDate: play.StartDate, endDate:play.EndDate }
    //});

    //dialogRef.afterClosed().subscribe(result => {
    //  // update play
    //  theater.Name = result;
    //  this.playAPIService.UpdatePlay(play).subscribe(result => {
    //  if (result.IsSucceeded) {

    //  } else {

    //  }
    //});
    //});
  }

}
//@Component({
//  selector: 'play-editor',
//  templateUrl: 'play-editor.html',
//})
//export class PlayEditor {
//  dialogTitle: string;
//  constructor(
//    public dialogRef: MatDialogRef<PlayEditor>,
//    @Inject(MAT_DIALOG_DATA) public data: PlayDialogData) {
//    this.dialogTitle = data != undefined ? "Add New Play" : "Edit Play: " + data.name;
//  }

//  onNoClick(): void {
//    this.dialogRef.close();
//  }

//}


export interface PlayDialogData {
  playId: string;
  name: string;
  startDate: string;
  endDate: string;
}
