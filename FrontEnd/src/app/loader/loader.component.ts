import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
})
export class LoaderComponent implements OnInit {
  constructor(private modalService: NgbModal) {}

  openVerticallyCentered(loader: any) {
    this.modalService.open(loader, {
      centered: true,
    });
  }

  ngOnInit(): void {}
}
