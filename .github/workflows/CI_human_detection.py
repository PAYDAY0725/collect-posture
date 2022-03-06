name: OAK build test

on:
  push:

jobs:
  build-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2

      - name: setup python3
        uses: actions/setup-python@v2
        with:
          python-version: '3.8'

      - name: Install dependencies
        run: |
	  python3 install_requirements.py
	  pip install pytest

      - name: build
        run: pytest /main.py
