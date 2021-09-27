import { Injectable } from '@angular/core';
import { OperationOutput, Theater } from '../_models';
import { AuthenticationService } from './authentication.service';
import BaseAPIService from './baseAPI.service';

@Injectable()
export class TheaterAPIService {
  constructor(
    private baseAPIService: BaseAPIService,
    private authenticationService: AuthenticationService
  ) {

  }

  public AddTheater(theater: Theater) {
    return this.baseAPIService.postCall<OperationOutput>("api/Theater/AddTheater", theater);
  }

  public UpdateTheater(theater: Theater) {
    return this.baseAPIService.postCall<OperationOutput>("api/Theater/UpdateTheater", theater);
  }

  public DeleteTheater(theaterId: number) {
    return this.baseAPIService.deleteCall<OperationOutput>("api/Theater/DeleteTheater", { theaterId: theaterId });
  }
  public GetTheater(theaterId: number) {
    return this.baseAPIService.getCall<Theater>("api/Theater/GetTheater", { theaterId: theaterId });
  }
  public GetFilteredTheaters(theaterName: string) {
    var input = {
      Name: theaterName
    };
    return this.baseAPIService.postCall<Theater[]>("api/Theater/GetFilteredTheaters", input);
  }

}
