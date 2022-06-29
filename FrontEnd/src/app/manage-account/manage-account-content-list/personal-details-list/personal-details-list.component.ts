import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { User } from 'src/app/models/UserData/user.item';
import { UserData } from 'src/app/view-models/userdata.item';

import AuthHelper from '../../../utils/authHelper';
import ImageHelper from '../../../utils/imageHelper';

import { AuthorizationService } from '../../../services/authorization.service';
import { NgbDate, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Country } from 'src/app/models/Location/country.item';
import { City } from 'src/app/models/Location/city.item';
import { ThrowStmt } from '@angular/compiler';
import { FormControl, Validators } from '@angular/forms';
import { MainDataService } from 'src/app/services/main-data.service';

@Component({
  selector: 'app-personal-details-list',
  templateUrl: './personal-details-list.component.html',
  styleUrls: ['./personal-details-list.component.css'],
})
export class PersonalDetailsListComponent implements OnInit {
  isEditing: boolean[] = [];
  isDisabled: boolean[] = [];

  imageHelper: any = ImageHelper;

  isEmailSent: boolean = false;
  letterAction: boolean = false;

  errorMessage: string = '';
  letterMessage: string = '';

  tempValue: string = '';
  tempLastName: string = '';
  tempBirthDate: NgbDate | null = null;

  zipCode: string | null = null;
  addressText: string = '';
  country: string = '';
  city: string = '';

  tempNationalityCheck: string = '';

  user: UserData = new UserData();

  check: boolean = false;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public authService: AuthorizationService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  setCondition(id: number): void {
    this.isEditing[id] = !this.isEditing[id];
  }

  editButtonClick(id: number, value: any): void {
    if (id == 6 || id == 8) {
      this.getCountries(id);
    }

    if (id == 1) {
      this.tempValue = this.user.firstName;
      this.tempLastName = this.user.lastName;
    } else if (id == 5) {
      this.tempBirthDate = value;
    } else if (id == 6) {
      this.tempNationalityCheck = this.user.nationality;
      this.user.nationality = '-1';
    } else if (id == 8) {
      this.zipCode = this.user.zipCode;
      this.addressText = this.user.addressText;
      this.country = this.user.country.title;
      this.city = this.user.city.title;
    } else {
      this.tempValue = value;
    }

    this.setCondition(id);
    this.setConditionEditButtons(id, true);
  }

  saveButtonClick(id: number): void {
    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  sendInfoLetter(): void {
    fetch(
      `https://localhost:44381/api/notifications/sendnotification?email=${AuthHelper.getLogin()}&message=${
        this.letterMessage
      }&action=${this.letterAction}`,
      {
        method: 'GET',
      }
    )
      .then((r) => r.json())
      .then(async (data) => {
        if (data.code === 200) {
        } else {
          alert(data.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  cancelButtonClick(id: number): void {
    if (id == 0) {
      this.user.title = this.tempValue;
    } else if (id == 1) {
      this.user.firstName = this.tempValue;
      this.user.lastName = this.tempLastName;
    } else if (id == 2) {
      this.user.displayName = this.tempValue;
    } else if (id == 3) {
      this.user.newEmail = this.tempValue;
    } else if (id == 4) {
      this.user.phoneNumber = this.tempValue;
    } else if (id == 5) {
      this.user.pBirthDate = this.tempBirthDate;
    } else if (id == 6) {
      this.user.nationality = this.tempNationalityCheck;
    } else if (id == 7) {
      this.user.genderId = +this.tempValue;
    } else if (id == 8) {
      this.user.zipCode = this.zipCode;
      this.user.addressText = this.addressText;
      this.user.country.title = this.country;
      this.user.city.title = this.city;
    }

    this.setCondition(id);
    this.setConditionEditButtons(id, false);
  }

  setConditionEditButtons(id: number, value: boolean): void {
    if (value == false) {
      this.check = false;
      if (id == 0) {
        let title = document.getElementById('title');
        if (title !== null) {
          title.classList.remove('invalid');
        }
      } else if (id == 1) {
        let firstName = document.getElementById('firstName');
        if (firstName !== null) {
          firstName.classList.remove('invalid');
        }

        let lastName = document.getElementById('lastName');
        if (lastName !== null) {
          lastName.classList.remove('invalid');
        }
      } else if (id == 2) {
        let userName = document.getElementById('userName');
        if (userName !== null) {
          userName.classList.remove('invalid');
        }
      } else if (id == 3) {
        let email = document.getElementById('email');
        if (email !== null) {
          email.classList.remove('invalid');
        }
      } else if (id == 4) {
        let phoneNumber = document.getElementById('phone-number');
        if (phoneNumber !== null) {
          phoneNumber.classList.remove('invalid');
        }
      } else if (id == 5) {
        this.user.pBirthDate = this.tempBirthDate;
      } else if (id == 6) {
        this.user.nationality = this.tempValue;
      } else if (id == 7) {
        this.user.genderId = +this.tempValue;
      } else if (id == 8) {
        this.user.zipCode = this.zipCode;
        this.user.addressText = this.addressText;
        this.user.country.title = this.country;
        this.user.city.title = this.city;
      }
    }
    for (let i = 0; i < this.isDisabled.length; i++) {
      if (i !== id) {
        this.isDisabled[i] = value;
      }
    }
  }

  saveTitle(id: number): void {
    if (this.user.title.length < 1) {
      this.check = true;
      let title = document.getElementById('title');
      if (title !== null) {
        title.classList.add('invalid');
      }
      return;
    }

    let user = {
      title: this.user.title,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/edittitle', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.title = response.resUser.title;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);

          this.letterMessage = `Your profile title was successfully changed to [${this.user.title}]!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save title error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  saveName(id: number): void {
    if (this.user.firstName.length < 1) {
      this.check = true;
      let firstName = document.getElementById('firstName');
      if (firstName !== null) {
        firstName.classList.add('invalid');
      }
      return;
    }

    if (this.user.lastName.length < 1) {
      this.check = true;
      let lastName = document.getElementById('lastName');
      if (lastName !== null) {
        lastName.classList.add('invalid');
      }
      return;
    }

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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

          this.letterMessage = `Your profile name was successfully changed to [${this.user.firstName} ${this.user.lastName}]!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save name error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  saveDisplayName(id: number): void {
    if (this.user.displayName.length < 1) {
      this.check = true;
      let displayName = document.getElementById('displayName');
      if (displayName !== null) {
        displayName.classList.add('invalid');
      }
      return;
    }

    let user = {
      displayName: this.user.displayName,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editdisplayname', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.displayName = response.resUser.displayName;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);

          this.letterMessage = `Your profile display name was successfully changed to [${this.user.displayName}]!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save display name error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  sendChangingEmailLetter(id: number): void {
    if (!this.user.newEmail.match('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$')) {
      this.check = true;
      let email = document.getElementById('email');
      if (email !== null) {
        email.classList.add('invalid');
      }
      return;
    }

    fetch(
      `https://localhost:44381/api/auth/isemailregistered?email=${this.user.newEmail}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200 && data.isExisted == false) {
          fetch(
            `https://localhost:44381/api/codes/generatechangingemailcode?email=${
              this.user.newEmail
            }&oldEmail=${AuthHelper.getLogin()}`,
            {
              method: 'GET',
              headers: {
                'Content-Type': 'application/json; charset=utf-8',
                Accept: 'application/json',
                Authorization:
                  AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
              },
            }
          )
            .then((r) => r.json())
            .then((data) => {
              if (data.code === 200) {
                this.saveButtonClick(id);
                this.isEmailSent = true;
              } else {
                alert('Error generating reset link');
              }
            })
            .catch((ex) => {
              this.mainDataService.alertContent = ex;
              this.modalService.open(this.alert);
            });
        } else {
          alert(data.message);
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  savePhoneNumber(id: number): void {
    if (
      !this.user.phoneNumber.match(
        '^[+]?[(]?[0-9]{3}[)]?[-s.]?[0-9]{3}[-s.]?[0-9]{4,6}$'
      )
    ) {
      this.check = true;
      let phoneNumber = document.getElementById('phone-number');
      if (phoneNumber !== null) {
        phoneNumber.classList.add('invalid');
      }
      return;
    }

    let user = {
      phoneNumber: this.user.phoneNumber,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editphonenumber', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.phoneNumber = response.resUser.phoneNumber;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);

          this.letterMessage = `Your profile phone number was successfully changed to [${this.user.phoneNumber}]!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save phone number error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  saveBirthDate(id: number): void {
    if (this.user.pBirthDate?.year) {
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
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

            this.letterMessage = `Your profile birth date was successfully changed to [${this.user.pBirthDate}]!`;
            this.letterAction = false;
            this.sendInfoLetter();
          } else {
            alert('Save birth date error!');
          }
        })
        .catch((ex) => {
          this.mainDataService.alertContent = ex;
          this.modalService.open(this.alert);
        });
    } else {
      alert('Choose a date');
    }
  }

  saveNationality(id: number): void {
    if (this.user.nationality == '-1') {
      alert('Select your nationality!');
      return;
    }

    let user = {
      nationality: this.user.nationality,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editnationality', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.nationality = response.resProfile.nationality;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);

          this.letterMessage = `Your profile nationality was successfully changed to [${this.user.nationality}]!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save nationality error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  saveGender(id: number): void {
    if (this.user.genderId != 1 && this.user.genderId != 2) {
      alert('Select your gender!');
      return;
    }

    let user = {
      genderId: this.user.genderId,
      email: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/userdataeditor/editgender', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.user.genderId = response.resProfile.genderId;
          this.setCondition(id);
          this.setConditionEditButtons(id, false);

          this.letterMessage = `Your profile gender was successfully changed!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save gender error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  saveAddress(id: number): void {
    if (this.user.addressText.length < 1) {
      this.check = true;
      let address = document.getElementById('address');
      if (address !== null) {
        address.classList.add('invalid');
      }
    }

    if (this.user.city.title.length < 1) {
      this.check = true;
      let city = document.getElementById('city');
      if (city !== null) {
        city.classList.add('invalid');
      }
    }

    if (this.check) {
      return;
    }

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
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
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

          this.letterMessage = `Your profile address was successfully changed!`;
          this.letterAction = false;
          this.sendInfoLetter();
        } else {
          alert('Save address error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  initializeBoolArray(): void {
    for (let i = 0; i < 9; i++) {
      this.isDisabled[i] = false;
    }
  }

  async getCurrentUser(): Promise<void> {
    if (!this.authService.isGotData) {
      await this.authService.refreshAuth();
    }
    fetch(
      'https://localhost:44381/api/users/getuser?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.authService.isGotData = true;
          this.authService.setUserImage(response.user.profile.image?.path);

          this.user.addressText = response.user.profile?.address?.addressText;
          this.user.zipCode = response.user.profile?.address?.zipCode;
          this.user.country.title =
            response.user.profile?.address?.country?.title;
          this.user.city.title = response.user.profile?.address?.city?.title;
          this.user.title = response.user.title;
          this.user.firstName = response.user.firstName;
          this.user.lastName = response.user.lastName;
          this.user.email = response.user.email;
          this.user.displayName = response.user.displayName;
          this.user.phoneNumber = response.user.phoneNumber;
          if (response.user.profile.birthDate) {
            this.user.pBirthDate = response.user.profile.birthDate.substring(
              0,
              response.user.profile.birthDate.indexOf('T')
            );
          }
          this.user.nationality = response.user.profile.nationality;
          this.user.genderId = response.user.profile.genderId;
        } else {
          alert('Get current user error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
            let countriesSelect = document.getElementById('countrySelect');
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
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  async ngOnInit(): Promise<void> {
    this.initializeBoolArray();
    await this.getCurrentUser();
  }
}
