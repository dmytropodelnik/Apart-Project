import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/UserData/user.item';
import { UserData } from 'src/app/view-models/userdata.item';

@Component({
  selector: 'app-personal-details-list',
  templateUrl: './personal-details-list.component.html',
  styleUrls: ['./personal-details-list.component.css']
})
export class PersonalDetailsListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  user: UserData = new UserData();

  constructor() {

  }

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  editButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, true);
  }

  saveButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  cancelButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  setConditionEditButtons(id: number, value: boolean): void {
    for (let i = 0; i < this.isDisabled.length; i++) {
      if (i !== id) {
        this.isDisabled[i] = value;
      }
    }
  }

  saveTitle(): void {

  }

  saveName(): void {

  }

  saveDisplayName(): void {

  }

  saveEmail(): void {

  }

  savePhoneNumber(): void {

  }

  saveBirthDate(): void {
    if (this.user.pBirthDate) {
      this.user.birthDate = this.user.pBirthDate!.day + '/' + this.user.pBirthDate!.month + '/' +
                            this.user.pBirthDate!.year;
    } else {
      alert("Choose a date");
    }

  }

  saveNationality(): void {

  }

  saveGender(): void {

  }

  saveAddress(): void {

  }

  initializeBoolArray(): void {
    for (let i = 0; i < 9; i++) {
      this.isDisabled[i] = false;
    }
  }

  ngOnInit(): void {
    this.initializeBoolArray();
  }

}
