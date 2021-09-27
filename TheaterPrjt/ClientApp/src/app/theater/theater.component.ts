import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService, TheaterAPIService } from '@/_services';
import { Theater } from '@/_models';
import { AlertService } from '../_alert';

@Component({
  selector: 'app-theater',
  templateUrl: './theater.component.html'
})
export class TheaterComponent {
  public theaters: Theater[];
  public theaterName: string;
  public isAdmin: boolean = false;
  constructor(
    public router: Router,
    private alertService: AlertService,
    private theaterAPIService: TheaterAPIService,
    private authenticationService: AuthenticationService) {
    this.isAdmin = this.authenticationService.currentUserValue.isAdmin;
    this.GetFilteredTheaters();
  }

  public search() {
    return this.GetFilteredTheaters();
  }

  public add() {
    //const dialogRef = this.dialog.open(TheaterEditor, {
    //  width: '250px',
    //  disableClose: true
    //});

    //return dialogRef.afterClosed().subscribe(result => {
    //  //add new theater
    //});
  }

  private GetFilteredTheaters() {
    return this.theaterAPIService.GetFilteredTheaters(this.theaterName).subscribe((result) => {
      this.theaters = result;
    }, error => console.error(error));

  }

  public view(theater: Theater) {
    this.router.navigate(['/play'],
      { queryParams: { theaterId: theater.theaterId, theaterName: theater.theaterName } }
    );
  }
  public delete(theater: Theater) {
    return this.theaterAPIService.DeleteTheater(theater.theaterId).subscribe(result => {
      if (result.isSucceeded) {
        this.GetFilteredTheaters();
      } else {
        this.alertService.error(result.errorMessage);
      }
    });
  }

  public edit(theater: Theater) {
    //const dialogRef = this.dialog.open(TheaterEditor, {
    //  width: '250px',
    //  disableClose: true,
    //  data: { theaterId: theater.TheaterId, name: theater.Name }
    //});

    //dialogRef.afterClosed().subscribe(result => {
    //  // update theater
    //  theater.Name = result;
    //  this.theaterAPIService.UpdateTheater(theater);
    //});
  }

}
//@Component({
//  selector: 'theater-editor',
//  templateUrl: 'theater-editor.html',
//})
//export class TheaterEditor {
//  dialogTitle: string;
//  constructor(
//    public dialogRef: MatDialogRef<TheaterEditor>,
//    @Inject(MAT_DIALOG_DATA) public data: TheaterDialogData) {
//    this.dialogTitle = data != undefined ? "Add New Theater" : "Edit Theater: " + data.name;
//  }

//  onNoClick(): void {
//    this.dialogRef.close();
//  }

//}


export interface TheaterDialogData {
  theaterId: string;
  name: string;
}
