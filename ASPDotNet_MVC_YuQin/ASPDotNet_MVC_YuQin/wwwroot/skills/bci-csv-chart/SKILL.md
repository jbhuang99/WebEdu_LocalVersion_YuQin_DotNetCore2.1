---
name: bci-csv-chart
description: Replace or connect BCI brainwave CSV data for BCI-TimeSequenceLineChart-Dynamic.html without requiring the user to edit JavaScript. Use when the user asks to load new brainwave data, replace chart data, update TryBCI.csv, or make the BCI time-sequence line chart show a new CSV file.
compatibility: Visual Studio / GitHub Copilot / Codex, ASP.NET Core static files
---

# BCI CSV Chart Skill

## Role

You help users replace the data shown by `BCI-TimeSequenceLineChart-Dynamic.html` with a new `.csv` brainwave dataset. The user should only need to describe the target CSV in natural language.

## Target Files

- Chart page: `ASPDotNet_MVC_YuQin/ASPDotNet_MVC_YuQin/wwwroot/webCourse/common/BCI-TimeSequenceLineChart-Dynamic.html`
- Default CSV: `ASPDotNet_MVC_YuQin/ASPDotNet_MVC_YuQin/wwwroot/webCourse/common/TryBCI.csv`

## Expected CSV Columns

The chart accepts comma-separated or tab-separated CSV. It supports these brainwave columns:

```text
Att,Med,Delta,Theta,LowAlpha,HighAlpha,LowBeta,HighBeta,LowGamma,MiddleGamma
```

An optional time column may be included with one of these names:

```text
TimeSeqence,TimeSequence,Time
```

If no time column exists, generate x-axis labels from the row number.

## Workflow

1. Locate the user's new `.csv` file.
2. Confirm the CSV contains the expected brainwave columns. If the separator is tab instead of comma, keep it; the HTML can parse both.
3. Put the CSV in the same folder as the chart page.
4. If the user wants this file to be the default data source, rename or copy it to `TryBCI.csv`.
5. If the user wants to keep several datasets, leave the file name unchanged and open the page with `?csv=<file-name>.csv`.
6. Do not ask the user to learn or edit JavaScript for ordinary data replacement.
7. Verify by opening the chart page through the ASP.NET app or a static local server, because `fetch()` may be blocked when the HTML is opened directly from `file://`.

## Example Prompts

```text
将本解决方案中的 BCI-TimeSequenceLineChart-Dynamic.html 中的数据，替换成为新的 TryBCI.csv 文件中的脑机数据。
```

```text
把 my-bci-data.csv 放到图表里显示，不要改动图表样式。
```

```text
保留旧的 TryBCI.csv，新增 final-test.csv，并让页面通过 ?csv=final-test.csv 加载。
```

## Verification Checklist

- `BCI-TimeSequenceLineChart-Dynamic.html` no longer depends on a hard-coded full dataset.
- The page loads `TryBCI.csv` by default.
- CSV files with no time column still render with row-number x-axis labels.
- The legend shows all brainwave series from the CSV header.
- The animation starts automatically and the start, pause, reset, and speed controls still work.
