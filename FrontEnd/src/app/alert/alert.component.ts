import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {
  @Input() content: string | undefined;

  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.content = 'Example';
  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {}
}
