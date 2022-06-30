import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MailLetter } from 'src/app/models/mailletter.item';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-letter-creator',
  templateUrl: './letter-creator.component.html',
  styleUrls: ['./letter-creator.component.css'],
})
export class LetterCreatorComponent implements OnInit {
  sentLetters: MailLetter[] = [];

  newLetter: MailLetter = new MailLetter();

  choice: boolean = false;

  htmlLetterFile: File | null = null;

  page: number = 1;
  pageSize: number = 15;

  isCreated: boolean = false;

  lettersAmount: number = 0;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  setChoice(): void {
    this.choice = !this.choice;
  }

  collectElements(sentLetters: MailLetter[]): void {
    for (let item of sentLetters) {
      this.sentLetters?.push(item);
    }
  }

  sendLetterAgain(value: number): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/deals/sendbestdealsletteragain?letterId=' +
        value,
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.isCreated = true;
          this.getSentMails();
          this.mainDataService.alertContent =
            'Letter has been successfully sent!';
          this.modalService.open(this.alert);
        } else {
          this.mainDataService.alertContent =
            'Sending letter to subscribers error!';
          this.modalService.open(this.alert);
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  async createLetter(): Promise<void> {
    if (this.newLetter.title.length < 3) {
      this.mainDataService.alertContent =
        'Title must have at least 3 characters';
      this.modalService.open(this.alert);
      return;
    }
    if (this.newLetter.text.length < 10 && !this.choice) {
      this.mainDataService.alertContent =
        'Text must have at least 10 characters';
      this.modalService.open(this.alert);
      return;
    }

    let letter;

    if (this.choice) {
      if (this.htmlLetterFile == null) {
        this.mainDataService.alertContent = 'Upload a file!';
        this.modalService.open(this.alert);
        return;
      }
      // checking file type
      if (!['text/html', 'text/plain'].includes(this.htmlLetterFile.type)) {
        this.mainDataService.alertContent =
          'Only HTML and TXT files are allowed!';
        this.modalService.open(this.alert);
        return;
      }

      await this.uploadFile();
    }

    letter = {
      title: this.newLetter.title,
      text: this.newLetter.text,
      sender: AuthHelper.getLogin(),
    };

    fetch('https://apartmain.azurewebsites.net/api/deals/sendbestdealsletter', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(letter),
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.isCreated = true;
          this.newLetter.title = '';
          this.newLetter.text = '';
          this.getSentMails();
          this.mainDataService.alertContent =
            'Letter has been successfully sent!';
          this.modalService.open(this.alert);
        } else {
          this.mainDataService.alertContent =
            'Sending letter to subscribers error!';
          this.modalService.open(this.alert);
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  async uploadFile(): Promise<void> {
    let fData = new FormData();
    if (this.htmlLetterFile != null) {
      fData.append('uploadedFile', this.htmlLetterFile);
    }

    await fetch(
      'https://apartmain.azurewebsites.net/api/fileuploader/uploadfile',
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: fData,
      }
    )
      .then((r) => r.json())
      .then(async (r) => {
        if (r.code === 200) {
          await this.readContentFromFile(fData);
          this.mainDataService.alertContent =
            'File has been successfully uploaded!';
          this.modalService.open(this.alert);
        } else {
          this.mainDataService.alertContent = 'Uploading file error!';
          this.modalService.open(this.alert);
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  async readContentFromFile(file: FormData): Promise<void> {
    await fetch(
      'https://apartmain.azurewebsites.net/api/files/readfilecontent',
      {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: file,
      }
    )
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.newLetter.text = r.letterText;
          this.mainDataService.alertContent =
            'File content has been successfully read!';
          this.modalService.open(this.alert);
        } else {
          this.mainDataService.alertContent =
            'Reading content from file error!';
          this.modalService.open(this.alert);
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      this.htmlLetterFile = files.item(0);
    }
  }

  getSentMails(value: boolean = false): void {
    if (value) {
      this.page++;
    }

    let url = `https://apartmain.azurewebsites.net/api/deals/getmails?page=${this.page}&pageSize=${this.pageSize}`;
    if (this.isCreated) {
      url = 'https://apartmain.azurewebsites.net/api/deals/getmails';
      this.isCreated = false;
    }

    fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          if (value) {
            this.collectElements(r.sentLetters);
          } else {
            if (r.sentLetters.length <= 15) {
              this.sentLetters = r.sentLetters;
            } else if (this.sentLetters.length / this.page < 15) {
              this.sentLetters?.push(r.lastLetter);
            }
          }
          this.lettersAmount = r.amount;
        } else {
          this.mainDataService.alertContent = 'Getting mails error!';
          this.modalService.open(this.alert);
        }
      })
      .catch((err) => {
        this.mainDataService.alertContent = err;
        this.modalService.open(this.alert);
      });
  }

  ngOnInit(): void {
    this.getSentMails();
  }
}
