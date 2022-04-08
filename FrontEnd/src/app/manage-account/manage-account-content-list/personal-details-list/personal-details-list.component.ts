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
    if (id == 6 || id == 8) {
      this.getCountries(id);
    }

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
    let user = {
      title: this.user.title,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/edittitle', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editname', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      displayName: this.user.displayName,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editdisplayname', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      newEmail: this.user.newEmail,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editemail', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      phoneNumber: this.user.phoneNumber,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editphonenumber', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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

        let user = {
          birthDate: this.user.birthDate,
          email: AuthHelper.getLogin(),
        };

      fetch('https://localhost:44381/api/userdataeditor/editbirthdate', {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: 'Bearer ' + AuthHelper.getToken(),
        },
        body: JSON.stringify(user),
      })
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.user.pBirthDate = response.resProfile.birthDate.substring(
              0,
              response.resProfile.birthDate.indexOf('T')
            );
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
    let user = {
      nationality: this.user.nationality,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editnationality', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      genderId: this.user.genderId,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editgender', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
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
    let user = {
      addressText: this.user.addressText,
      country: this.user.country.title,
      city: this.user.city.title,
      zipCode: this.user.zipCode,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editaddress', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.addressText = response.resAddress.addressText;
          this.user.zipCode = response.resAddress.zipCode;
          this.user.country.title = response.resAddress.country.title;
          this.user.city.title = response.resAddress.city.title;
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
    fetch(
      'https://localhost:44381/api/users/getuser?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.addressText = response.user.profile.address.addressText;
          this.user.zipCode = response.user.profile.address.zipCode;
          this.user.country.title = response.user.profile.address.country.title;
          this.user.city.title = response.user.profile.address.city.title;
          this.user.title = response.user.title;
          this.user.firstName = response.user.firstName;
          this.user.lastName = response.user.lastName;
          this.user.email = response.user.email;
          this.user.displayName = response.user.displayName;
          this.user.phoneNumber = response.user.phoneNumber;
          this.user.pBirthDate = response.user.profile.birthDate.substring(
            0,
            response.user.profile.birthDate.indexOf('T')
          );
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

  getCountries(id: number): void {
    fetch(`https://localhost:44381/api/countries/getcountries`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          if (id == 6) {
            let nationalitiesSelect =
              document.getElementById('nationalitySelect');
            let newOption;
            for (let item of data.countries) {
              newOption = new Option(item.title, item.title);
              nationalitiesSelect?.append(newOption);
            }
          } else if (id == 8) {
            let countriesSelect =
              document.getElementById('countrySelect');
            let newOption;
            for (let item of data.countries) {
              newOption = new Option(item.title, item.title);
              countriesSelect?.append(newOption);
            }
          }
        } else {
          alert('Fetch error!');
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
