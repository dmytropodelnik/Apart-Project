import { Input, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-manage-account-content',
  templateUrl: './manage-account-content.component.html',
  styleUrls: ['./manage-account-content.component.css'],
})
export class ManageAccountContentComponent implements OnInit {
  @Input() content: string | undefined;

  constructor() {
    this.content = 'personal-details';
  }

  setContent(newContent: string) {
    this.content = newContent;
  }

  ngOnInit(): void {}
}
