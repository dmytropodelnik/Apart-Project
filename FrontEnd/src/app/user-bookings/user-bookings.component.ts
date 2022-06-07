import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-user-bookings',
  templateUrl: './user-bookings.component.html',
  styleUrls: ['./user-bookings.component.css'],
})
export class UserBookingsComponent implements OnInit {
  constructor(private modalService: NgbModal) {}

  openVerticallyCentered(content: any) {
    this.modalService.open(content, {
      centered: true,
    });
  }

  ngOnInit(): void {}
}
