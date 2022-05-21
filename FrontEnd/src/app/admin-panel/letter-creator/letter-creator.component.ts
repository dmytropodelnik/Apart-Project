import { Component, OnInit } from '@angular/core';
import { MailLetter } from 'src/app/models/mailletter.item';

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

  constructor() {}

  setChoice(): void {
    this.choice = !this.choice;
  }

  async createLetter(): Promise<void> {
    let letter;

    if (this.choice) {
      if (this.htmlLetterFile == null) {
        alert('Upload a file!');
        return;
      }
      // checking file type
      if (!['text/html', 'text/plain'].includes(this.htmlLetterFile.type)) {
        alert('Only HTML and TXT files are allowed!');
        return;
      }

      await this.uploadFile();
    }

    letter = {
      title: this.newLetter.title,
      text: this.newLetter.text,
      sender: AuthHelper.getLogin(),
    };

    fetch('https://localhost:44381/api/deals/sendbestdealsletter', {
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
          this.sentLetters = r.sentLetters;
          alert('Letter has been successfully sent!');
        } else {
          alert('Sending letter to subscribers error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  async uploadFile(): Promise<void> {
    let fData = new FormData();
    if (this.htmlLetterFile != null) {
      fData.append('uploadedFile', this.htmlLetterFile);
    }

    await fetch('https://localhost:44381/api/fileuploader/uploadfile', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: fData,
    })
      .then((r) => r.json())
      .then(async (r) => {
        if (r.code === 200) {
          await this.readContentFromFile(fData);
          alert('File has been successfully uploaded!');
        } else {
          alert('Uploading file error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  async readContentFromFile(file: FormData): Promise<void> {
    await fetch('https://localhost:44381/api/files/readfilecontent', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: file,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.newLetter.text = r.letterText;
          alert('File content has been successfully read!');
        } else {
          alert('Reading content from file error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      this.htmlLetterFile = files.item(0);
    }
  }

  getSentMails(): void {
    fetch('https://localhost:44381/api/deals/getmails', {
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
          this.sentLetters = r.sentLetters;
        } else {
          alert('Getting mails error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  ngOnInit(): void {
    this.getSentMails();
  }
}
