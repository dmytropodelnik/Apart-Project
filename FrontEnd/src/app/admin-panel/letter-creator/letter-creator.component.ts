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
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.sentLetters = r.sentLetters;
          alert('File has been successfully uploaded!');
        } else {
          alert('Uploading file error!');
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
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: fData,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.readContentFromFile(fData);
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
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: file,
    })
      .then((r) => r.json())
      .then((r) => {
        if (r.code === 200) {
          this.newLetter.text = r.letterText;
          alert('File has been successfully uploaded!');
        } else {
          alert('Uploading file error!');
        }
      })
      .catch((err) => {
        alert(err);
      });
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      // checking file type
      if (!['text/html', 'text/plain'].includes(files.item(0)!.type)) {
        alert('Only HTML files are allowed!');
        return;
      }
      this.htmlLetterFile = files.item(0);
    }
  }

  ngOnInit(): void {}
}
