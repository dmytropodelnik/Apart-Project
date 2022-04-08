import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/UserData/user.item';
import { UserData } from 'src/app/view-models/userdata.item';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';

@Component({
  selector: 'app-personal-details-list',
  templateUrl: './personal-details-list.component.html',
  styleUrls: ['./personal-details-list.component.css'],
})
export class PersonalDetailsListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  user: UserData = new UserData();

  constructor(private authService: AuthorizationService) {}

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

  saveTitle(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/edittitle', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.title = response.resUser.title;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save title error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveName(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editname', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.firstName = response.resUser.firstName;
          this.user.lastName = response.resUser.lastName;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save name error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveDisplayName(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editdisplayname', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.displayName = response.resUser.displayName;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save display name error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveEmail(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editemail', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.email = response.resUser.email;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save email error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  savePhoneNumber(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editphonenumber', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.phoneNumber = response.resUser.phoneNumber;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save phone number error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveBirthDate(id: number): void {
    if (this.user.pBirthDate) {
      this.user.birthDate =
        this.user.pBirthDate!.day +
        '/' +
        this.user.pBirthDate!.month +
        '/' +
        this.user.pBirthDate!.year;

      fetch('https://localhost:44381/api/userdataeditor/editbirthdate', {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: 'Bearer ' + AuthHelper.getToken(),
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.user.birthDate = response.resProfile.birthDate;
            this.setCondition(id);
            this.setConditionEditButtons(id, false);
          } else {
            alert('Save birth date error!');
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    } else {
      alert('Choose a date');
    }
  }

  saveNationality(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editnationality', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.nationality = response.resProfile.nationality;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save nationality error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveGender(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editgender', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.genderId = response.resProfile.genderId;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save gender error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  saveAddress(id: number): void {
    fetch('https://localhost:44381/api/userdataeditor/editaddress', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(this.user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.address = response.resAddress;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);
        } else {
          alert('Save address error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 9; i++) {
      this.isDisabled[i] = false;
    }
  }

  getCurrentUser(): void {
    fetch('https://localhost:44381/api/users/getuser?email=' + AuthHelper.getLogin(), {
      method: 'GET',
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.address = response.user.profile.address;
          this.user.title = response.user.title;
          this.user.firstName = response.user.firstName;
          this.user.lastName = response.user.lastName;
          this.user.email = response.user.email;
          this.user.phoneNumber = response.user.phoneNumber;
          this.user.pBirthDate = response.user.profile.birthDate;
          this.user.nationality = response.user.profile.nationality;
          this.user.genderId = response.user.profile.genderId;
        } else {
          alert('Get current user error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  ngOnInit(): void {
    this.initializeBoolArray();
    this.getCurrentUser();
  }
}
