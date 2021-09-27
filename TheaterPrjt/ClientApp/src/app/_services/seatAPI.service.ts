import { OperationOutput, Seat, SeatDetail } from '../_models';
import BaseAPIService from './baseAPI.service';

import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
@Injectable()
export class SeatAPIService {
  constructor(
    private baseAPIService: BaseAPIService,
    private authenticationService: AuthenticationService
  ) {

  }
  public AddSeat(seat: Seat) {
    return this.baseAPIService.postCall<OperationOutput>("api/Seat/AddSeat", seat);
  }

  public UpdateSeat(seat: Seat) {
    return this.baseAPIService.postCall<OperationOutput>("api/Seat/UpdateSeat", seat);
  }

  public DeleteSeat(seatId: number) {
    return this.baseAPIService.deleteCall<OperationOutput>("api/Seat/DeleteSeat", { seatId: seatId });
  }
  public GetSeat(seatId: number) {
    return this.baseAPIService.getCall("api/Seat/GetSeat", { seatId: seatId });
  }
  public GetFilteredSeats(theaterId: number, playId: number, seatNumber: string) {
    var input = {
      TheaterId: theaterId, PlayId: playId, SeatNumber: seatNumber,
      LoggedInUserId: this.authenticationService.currentUserValue.userId
    }
    return this.baseAPIService.postCall<SeatDetail[]>("api/Seat/GetFilteredSeats", input);
  }

  public ReserveSeat(seatId: number, playId: number) {
    return this.baseAPIService.getCall<OperationOutput>("api/Seat/ReserveSeat", { seatId: seatId, playId: playId });
  }

  public ConfirmSeat(seatPlayId: number) {
    return this.baseAPIService.getCall<OperationOutput>("api/Seat/ConfirmSeat", { seatPlayId: seatPlayId });
  }

  public DeclineSeat(seatPlayId: number) {
    return this.baseAPIService.getCall<OperationOutput>("api/Seat/DeclineSeat", { seatPlayId: seatPlayId });
  }
}
