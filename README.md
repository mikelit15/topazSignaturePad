# Signature Capture App

A simple Windows Forms application in C# for capturing signatures using a Topaz LCD signature pad. Displays instructions and buttons on the pad’s LCD and on the PC form, captures the signature, and saves it as a PNG file.

---

## Table of Contents

1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Usage](#usage)
5. [Code Overview](#code-overview)
6. [Directory Structure](#directory-structure)
7. [Configuration & Assets](#configuration--assets)
8. [Troubleshooting](#troubleshooting)

---

## Features

* **LCD Graphics & Hotspots**: Displays background and button graphics on the signature pad’s LCD, with interactive “Clear” and “Done” hotspots.
* **Signature Panel**: Hosts the pad’s drawing area within a WinForms panel so users see their strokes on the PC form.
* **Save as PNG**: Captures the signature at high resolution (2000×700 px) and saves it to `signature.png` in the application folder.
* **Status Feedback**: Shows prompts on both the pad’s LCD and via standard Windows MessageBoxes.
* **Auto-Exit**: Exits automatically once a valid signature is saved.

---

## Prerequisites

* **Operating System**: Windows 10 or later
* **Framework**: .NET Framework 4.7.2 (or newer)
* **IDE**: Visual Studio 2019 / 2022 or equivalent
* **Hardware**: Topaz LCD signature pad (e.g., T-LBK460-HSB)
* **Driver & SDK**:
  * Topaz signature pad driver installed (https://www.sigpluspro.com/sigplus.html)
  * Topaz SigPlus.NET SDK (`SigPlusNET.dll`) (https://www.topazsystems.com/sdks/sigplusnet.html)

---

## Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/mikelit15/topazSignaturePad.git
   cd topazSignaturePad
   ```

2. **Prepare SDK Reference**

   * Copy `SigPlusNET.dll` into the project’s `libs/` directory.
   * In Visual Studio, right-click **References** → **Add Reference…** → **Browse** → select `SigPlusNET.dll`.


3. **Build the Solution**

   * Open `SignatureCaptureApp.sln` in Visual Studio.
   * Set target framework to **.NET Framework 4.7.2** (or higher).
   * Build → **Build Solution**.

---

## Usage

1. **Run the Application**

   * Launch `SignatureCaptureApp.exe`.
   * The application window centers on screen and automatically initializes the pad.

2. **Capture Your Signature**

   * On the pad’s LCD you’ll see the *Sign* prompt.
   * Sign with the stylus in the designated area.
   * Tap **Done** on the pad or click the **Done** button on the form.

3. **Save & Exit**

   * If a signature is detected, it saves `signature.png` to the application folder.
   * A “Signature capture complete.” message appears briefly on the pad’s LCD.
   * The app then exits automatically.

4. **Clear & Retry**

   * Tap **Clear** on the pad or the **Clear** button on the form to reset the pad and try again.

---

## Code Overview

* **Program.cs**
  Entry point that initializes WinForms and starts `Form1`.&#x20;

* **Form1.Designer.cs**

  * Defines UI elements:

    * `signaturePanel` hosts the Topaz control
    * `sigPlusNET1` (Topaz SigPlusNET control)
    * `clearButton` & `doneButton`&#x20;

* **Form1.cs**

  * Loads BMP assets from `imgs/` on startup
  * Configures the pad (graphics, hotspots, capture window) in `cmdStart_Click`
  * Handles pen-up events to clear or save the signature (`sigPlusNET1_PenUp`)
  * Saves the signature with custom sizing/justification to `signature.png` in `saveImage()`
  * Cleans up hardware state on form closing&#x20;

* **Form1.resx**

  * Contains the application icon and other embedded resources.

---

## Directory Structure

```
SignatureCaptureApp/
├── libs/
│   └── SigPlusNET.dll
├── imgs/
│   ├── Sign.bmp
│   ├── Done.bmp
│   ├── Clear.bmp
│   └── Please.bmp
├── Program.cs
├── Form1.Designer.cs
├── Form1.cs
├── Form1.resx
└── README.md
```

---

## Configuration & Assets

* **BMP Images**:

  * `Sign.bmp` should match your pad’s full resolution (e.g., 240×64 px).
  * `Done.bmp` & `Clear.bmp` sizes/positions are hard-coded (`LCDSendGraphic(…199,48, done)` etc.)—adjust coordinates if needed.
  * `Please.bmp` is displayed when no signature is detected.

* **Output**:

  * `signature.png` (2000×700 px) is generated in the application folder.

---

## Troubleshooting

* **DLL Load Errors**

  * Ensure `SigPlusNET.dll` is copied to the application’s output directory and matches your OS architecture (x86 vs. x64).
  * Install the Topaz pad driver from Topaz’s website.

* **No Signature Captured**

  * Verify the pad is in capture mode (indicator LED on).
  * Make sure your pen strokes occur within the active window (`SetSigWindow` coordinates).

* **Incorrect Graphics Display**

  * Check that your BMP dimensions and hotspot coordinates align with your pad model’s LCD resolution.

