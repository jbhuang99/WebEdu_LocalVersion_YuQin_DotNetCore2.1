function fnStringSimilarity(a, b) {
    // Levenshtein distance
    const m = a.length, n = b.length;
    if (m === 0 || n === 0) return 0;
    const dp = Array.from({ length: m + 1 }, () => Array(n + 1).fill(0));
    for (let i = 0; i <= m; i++) dp[i][0] = i;
    for (let j = 0; j <= n; j++) dp[0][j] = j;
    for (let i = 1; i <= m; i++) {
        for (let j = 1; j <= n; j++) {
            if (a[i - 1] === b[j - 1]) {
                dp[i][j] = dp[i - 1][j - 1];
            } else {
                dp[i][j] = Math.min(
                    dp[i - 1][j] + 1,
                    dp[i][j - 1] + 1,
                    dp[i - 1][j - 1] + 1
                );
            }
        }
    }
    const distance = dp[m][n];
    // Similarity: 1 - normalized distance
    return 1 - distance / Math.max(m, n);
}

// Example usage:
//console.log(fnStringSimilarity("kitten", "sitting")); // Output: ~0.571
//console.log(fnStringSimilarity("hello", "hello"));    // Output: 1
//---------------------------------------------------------------

// Compare two images by pixel similarity (returns a value between 0 and 1)
/**
async function fnPixelImageSimilarity(img1Src, img2Src) {
    // Load images
   
    const loadImage = src => new Promise(resolve => {
        const img = new Image();
        img.crossOrigin = "Anonymous";
        img.onload = () => resolve(img);
        img.src = src;
    });
    
    const [img1, img2] = await Promise.all([loadImage(img1Src), loadImage(img2Src)]);
     
    // Set canvas size to smallest image
    const width = Math.min(img1.width, img2.width);
    const height = Math.min(img1.height, img2.height);
  
    // Draw images to canvases
    const canvas1 = document.createElement('canvas');
    const canvas2 = document.createElement('canvas');
    canvas1.width = canvas2.width = width;
    canvas1.height = canvas2.height = height;
      
    const ctx1 = canvas1.getContext('2d');
    const ctx2 = canvas2.getContext('2d');
    ctx1.drawImage(img1, 0, 0, width, height);
    ctx2.drawImage(img2, 0, 0, width, height);
    
    // Get pixel data
    const data1 = ctx1.getImageData(0, 0, width, height).data;
    const data2 = ctx2.getImageData(0, 0, width, height).data;
    
    // Compare pixels
    let same = 0;
    for (let i = 0; i < data1.length; i += 4) {
        // Compare RGBA
        if (
            data1[i] === data2[i] &&
            data1[i + 1] === data2[i + 1] &&
            data1[i + 2] === data2[i + 2] &&
            data1[i + 3] === data2[i + 3]
        ) {
            same++;
        }
    }
   
    const total = data1.length / 4;
    return same / total; // Similarity ratio (0~1)
     alert(img1Src+img2Src);
}
**/
/** Example usage:
fnImageSimilarity('image1.png', 'image2.png').then(sim => {
  console.log('Similarity:', sim);
});
**/
/**
 function fnViewRealImage(sId) {
    open(document.getElementById(sId).src,"_blank");
    }

function fnShowAccuraySimilarityForImage() {
    img1Src=document.getElementById("sShowPixelImageFromAIGC").src;
    img2Src=document.getElementById("sShowPixelImageFromKnowledgebase").src;
    alert(img1Src+img2Src);
    document.getElementById("sAccuracySimilarityForPixelImage").textContent=fnPixelImageSimilarity(img1Src,img2Src)
    }
 **/

function loadImage(url) {
  return new Promise((resolve, reject) => {
    const img = new Image();
    img.crossOrigin = "anonymous"; // 关键：启用跨域
    img.onload = () => resolve(img);
    img.onerror = () => reject(new Error("无法加载图片：" + url));
    img.src = url;
  });
}

async function getImageData(img, size = { width: 64, height: 64 }) {
  const canvas = document.createElement('canvas');
  const ctx = canvas.getContext('2d');
  canvas.width = size.width;
  canvas.height = size.height;
  ctx.drawImage(img, 0, 0, size.width, size.height);
  return ctx.getImageData(0, 0, size.width, size.height).data;
}

function computeMSE(data1, data2) {
  let sum = 0;
  for (let i = 0; i < data1.length; i++) {
    sum += Math.pow(data1[i] - data2[i], 2);
  }
  return sum / data1.length;
}

async function fnPixelImageSimilarity() {
  const url1 = document.getElementById('sPixelImageFromAIGC').value;
  const url2 = document.getElementById('sPixelImageFromKnowledgebase').value;
  const resultDiv = document.getElementById('sAccuracySimilarityForPixelImage');
  
  if (!url1 || !url2) {
    resultDiv.innerHTML = "请填写两个有效的图片链接。";
    return;
  }

  try {
    resultDiv.innerHTML = "正在加载并处理图片...";

    const [img1, img2] = await Promise.all([loadImage(url1), loadImage(url2)]);

    // 统一缩放为 64x64 灰度特征图（简化计算）
    const size = { width: 64, height: 64 };

    const imageData1 = await getImageData(img1, size);
    const imageData2 = await getImageData(img2, size);

    // 转为灰度亮度数组（Luma）
    const gray1 = [], gray2 = [];
    for (let i = 0; i < imageData1.length; i += 4) {
      const r = imageData1[i], g = imageData1[i+1], b = imageData1[i+2];
      gray1.push(0.299*r + 0.587*g + 0.114*b);
    }
    for (let i = 0; i < imageData2.length; i += 4) {
      const r = imageData2[i], g = imageData2[i+1], b = imageData2[i+2];
      gray2.push(0.299*r + 0.587*g + 0.114*b);
    }

    // 计算均方误差（MSE）
    const mse = computeMSE(gray1, gray2);

    // 归一化为相似度百分比（假设最大差异为 255^2）
    const maxMSE = 255 * 255;
    const similarity = Math.max(0, (1 - Math.sqrt(mse) / 255)) * 100;

    resultDiv.innerHTML = `
     <!-- <p><strong>MSE:</strong> ${mse.toFixed(2)}</p>-->
    <!--  <span>相似度：</strong>${similarity.toFixed(2)}%</span>-->
    <span>${similarity.toFixed(2)/100}</span>
    `;
  } catch (error) {
    console.error(error);
    resultDiv.innerHTML += `<p><strong>错误：</strong>${error.message}</p>`;
  }
}
//---------------------------------------------------------

//-------------------------------------------------------

// Compare two videos by sampling frames and averaging image similarity
/**
async function fnVideoSimilarity(video1Src, video2Src, frameCount = 10) {
    // Helper to load video and seek to a specific time, then get frame as image data
    async function getFrameData(videoSrc, time) {
        return new Promise(resolve => {
            const video = document.createElement('video');
            video.crossOrigin = "Anonymous";
            video.src = videoSrc;
            video.muted = true;
            video.currentTime = time;
            video.onloadeddata = () => {
                video.currentTime = time;
            };
            video.onseeked = () => {
                const canvas = document.createElement('canvas');
                canvas.width = video.videoWidth;
                canvas.height = video.videoHeight;
                const ctx = canvas.getContext('2d');
                ctx.drawImage(video, 0, 0, canvas.width, canvas.height);
                resolve(ctx.getImageData(0, 0, canvas.width, canvas.height).data);
            };
        });
    }

    // Helper to get duration of video
    async function getDuration(videoSrc) {
        return new Promise(resolve => {
            const video = document.createElement('video');
            video.src = videoSrc;
            video.onloadedmetadata = () => resolve(video.duration);
        });
    }

    // Get durations
    const [duration1, duration2] = await Promise.all([
        getDuration(video1Src),
        getDuration(video2Src)
    ]);
    const minDuration = Math.min(duration1, duration2);

    // Sample frames at equal intervals
    let similarities = [];
    for (let i = 0; i < frameCount; i++) {
        const time = (minDuration * i) / (frameCount - 1);
        const [frame1, frame2] = await Promise.all([
            getFrameData(video1Src, time),
            getFrameData(video2Src, time)
        ]);
        // Compare frames (pixel similarity)
        let same = 0;
        const len = Math.min(frame1.length, frame2.length);
        for (let j = 0; j < len; j += 4) {
            if (
                frame1[j] === frame2[j] &&
                frame1[j + 1] === frame2[j + 1] &&
                frame1[j + 2] === frame2[j + 2] &&
                frame1[j + 3] === frame2[j + 3]
            ) {
                same++;
            }
        }
        similarities.push(same / (len / 4));
    }
    // Average similarity across frames
    return similarities.reduce((a, b) => a + b, 0) / similarities.length;
}
**/
/** Example usage:
fnVideoSimilarity('video1.mp4', 'video2.mp4', 10).then(sim => {
    console.log('Video similarity:', sim); // Value between 0 and 1
});
**/
function getHistogram(ctx, width, height) {
  const imageData = ctx.getImageData(0, 0, width, height);
  const data = imageData.data;
  const hist = new Array(256).fill(0);

  for (let i = 0; i < data.length; i += 4) {
    const gray = (data[i] + data[i+1] + data[i+2]) / 3;
    hist[Math.round(gray)]++;
  }
  return hist;
}

function histogramDistance(hist1, hist2) {
  let distance = 0;
  for (let i = 0; i < 256; i++) {
    distance += Math.abs(hist1[i] - hist2[i]);
  }
  return distance / (256 * 255); // 归一化
}

async function extractKeyFrameHistogram(videoElement, url) {
  videoElement.src = url;
  await new Promise((resolve) => {
    videoElement.onloadeddata = resolve;
  });

  videoElement.currentTime = 1; // 跳到第1秒
  await new Promise((resolve) => {
    videoElement.ontimeupdate = () => resolve();
  });

  const canvas = document.getElementById('canvas');
  const ctx = canvas.getContext('2d');
  ctx.drawImage(videoElement, 0, 0, canvas.width, canvas.height);

  return getHistogram(ctx, canvas.width, canvas.height);
}

async function fnCompareVideos() {
  const url1 = document.getElementById("sVideoFromAIGC").value;
  const url2 = document.getElementById("sVideoFromKnowledgebase").value;
  const resultDiv = document.getElementById("sAccuracySimilarityForPixelVideo");

  try {
    const hist1 = await extractKeyFrameHistogram(document.getElementById("sShowVideoFromAIGC"), url1);
    const hist2 = await extractKeyFrameHistogram(document.getElementById("sShowVideoFromKnowledgebase"), url2);

    const distance = histogramDistance(hist1, hist2);
    const similarity = (1 - distance) ;

    resultDiv.innerHTML = `${similarity.toFixed(2)}`;
  } catch (e) {
    resultDiv.innerHTML = `错误: ${e.message}`;
  }
}
//---------------------------------------------------------
// Compare two SVG strings visually by rendering to canvas and comparing pixels
/**
async function fnSVGSimilarity(svg1, svg2, width = 200, height = 200) {
    // Helper: Render SVG string to canvas and get pixel data
    function renderSVGToCanvas(svg, width, height) {
        return new Promise(resolve => {
            const img = new Image();
            img.crossOrigin = "Anonymous";
            const svgBlob = new Blob([svg], { type: "image/svg+xml" });
            const url = URL.createObjectURL(svgBlob);
            img.onload = () => {
                const canvas = document.createElement('canvas');
                canvas.width = width;
                canvas.height = height;
                const ctx = canvas.getContext('2d');
                ctx.clearRect(0, 0, width, height);
                ctx.drawImage(img, 0, 0, width, height);
                const data = ctx.getImageData(0, 0, width, height).data;
                URL.revokeObjectURL(url);
                resolve(data);
            };
            img.src = url;
        });
    }

    // Render both SVGs
    const [data1, data2] = await Promise.all([
        renderSVGToCanvas(svg1, width, height),
        renderSVGToCanvas(svg2, width, height)
    ]);

    // Compare pixel data
    let same = 0;
    const len = Math.min(data1.length, data2.length);
    for (let i = 0; i < len; i += 4) {
        if (
            data1[i] === data2[i] &&
            data1[i + 1] === data2[i + 1] &&
            data1[i + 2] === data2[i + 2] &&
            data1[i + 3] === data2[i + 3]
        ) {
            same++;
        }
    }
    const total = len / 4;
    return same / total; // Similarity ratio (0~1)
}
**/
/** Example usage:
const svgA = `<svg width="100" height="100"><circle cx="50" cy="50" r="40" fill="red"/></svg>`;
const svgB = `<svg width="100" height="100"><circle cx="50" cy="50" r="40" fill="blue"/></svg>`;
fnSVGSimilarity(svgA, svgB).then(sim => {
    console.log('SVG similarity:', sim);
});
**/
async function svgToCanvas(url, width = 100, height = 100) {
  const canvas = document.createElement('canvas');
  canvas.width = width;
  canvas.height = height;
  const ctx = canvas.getContext('2d');

  const img = new Image();
  img.crossOrigin = 'anonymous';
  img.src = url + '#svg-view(1)';
  await new Promise(r => { img.onload = r; });

  ctx.drawImage(img, 0, 0, width, height);
  return canvas;
}

function getImageHash(ctx, w, h) {
  const imageData = ctx.getImageData(0, 0, w, h).data;
  let hash = 0;
  for (let i = 0; i < w * h * 4; i += 4) {
    const gray = (imageData[i] + imageData[i+1] + imageData[i+2]) / 3;
    hash = ((hash << 5) - hash) + gray;
    hash |= 0;
  }
  return hash;
}

async function fnShowAccuraySimilarityFor2D() {
  var url1=document.getElementById("s2DFromAIGC").value; 
  var url2=document.getElementById("s2DFromKnowledgebase").value;
  const canvas1 = await svgToCanvas(url1);
  const canvas2 = await svgToCanvas(url2);

  const hash1 = getImageHash(canvas1.getContext('2d'), 100, 100);
  const hash2 = getImageHash(canvas2.getContext('2d'), 100, 100);

  const diff = Math.abs(hash1 - hash2);
 // return 100 - Math.min(diff / 0x7fffffff * 100, 100);
 document.getElementById("sAccuracySimilarityFor2D").textContent=((100 - Math.min(diff / 0x7fffffff * 100, 100))/100).toFixed(2);
}
//---------------------------------------------------------
//Requires X3DOM or similar to render X3D in browser, then compare canvas pixels. (requires X3DOM and browser support).
async function fnX3DVisualSimilarity(x3d1, x3d2, width = 300, height = 300) {
    // Helper: Render X3D string to canvas and get pixel data
    async function renderX3DToCanvas(x3dString, width, height) {
        return new Promise(resolve => {
            // Create X3DOM scene
            const container = document.createElement('div');
            container.style.position = 'absolute';
            container.style.left = '-9999px';
            document.body.appendChild(container);
            container.innerHTML = x3dString;

            // Wait for X3DOM to render
            setTimeout(() => {
                const x3dElem = container.querySelector('x3d');
                if (!x3dElem) return resolve(null);
                const canvas = x3dElem.querySelector('canvas');
                if (!canvas) return resolve(null);

                // Draw canvas to image
                const tempCanvas = document.createElement('canvas');
                tempCanvas.width = width;
                tempCanvas.height = height;
                const ctx = tempCanvas.getContext('2d');
                ctx.drawImage(canvas, 0, 0, width, height);
                const data = ctx.getImageData(0, 0, width, height).data;
                document.body.removeChild(container);
                resolve(data);
            }, 1000); // Wait for rendering (may need to adjust)
        });
    }

    const [data1, data2] = await Promise.all([
        renderX3DToCanvas(x3d1, width, height),
        renderX3DToCanvas(x3d2, width, height)
    ]);
    if (!data1 || !data2) return 0;

    // Compare pixel data
    let same = 0;
    const len = Math.min(data1.length, data2.length);
    for (let i = 0; i < len; i += 4) {
        if (
            data1[i] === data2[i] &&
            data1[i + 1] === data2[i + 1] &&
            data1[i + 2] === data2[i + 2] &&
            data1[i + 3] === data2[i + 3]
        ) {
            same++;
        }
    }
    const total = len / 4;
    return same / total; // Similarity ratio (0~1)
}
/** Example usage:
 **/
//在前端 JavaScript 中，直接比较两个 MP3 音频文件的相似性（如语音内容、旋律、音色等）是一个复杂任务。浏览器原生 API 并不直接支持音频指纹或语音识别级别的相似度对比，但可以通过音频特征提取和简单波形对比实现基础的相似性分析。更高级的对比通常需要后端服务或第三方 AI API。
// 前端基础方案：对比波形能量（粗略）1.	读取文件并解码为 PCM 数据2.	对比波形能量或均值等简单特征
/**hTML 示例：
<input type="file" id="file1" accept="audio/mp3" />
<input type="file" id="file2" accept="audio/mp3" />
<button onclick="compareMp3Similarity()">比较相似性</button>
<div id="result"></div>
**/
/**
async function getPcmData(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = async function(e) {
            const arrayBuffer = e.target.result;
            const audioCtx = new (window.AudioContext || window.webkitAudioContext)();
            try {
                const audioBuffer = await audioCtx.decodeAudioData(arrayBuffer);
                // 只取第一个声道
                const pcm = audioBuffer.getChannelData(0);
                resolve(pcm);
            } catch (err) {
                reject(err);
            }
        };
        reader.onerror = reject;
        reader.readAsArrayBuffer(file);
    });
}

function calcEnergy(pcm) {
    let sum = 0;
    for (let i = 0; i < pcm.length; i++) {
        sum += Math.abs(pcm[i]);
    }
    return sum / pcm.length;
}

async function fnAudioSimilarity(audio1Src, audio2Src) {

    try {
        const [pcm1, pcm2] = await Promise.all([getPcmData(audio1Src), getPcmData(audio2Src)]);
        // 简单能量均值对比
        const energy1 = calcEnergy(pcm1);
        const energy2 = calcEnergy(pcm2);
        const similarity = 1 - Math.abs(energy1 - energy2) / Math.max(energy1, energy2);
        document.getElementById('result').textContent = '相似度（能量均值法，0~1）：' + similarity.toFixed(3);
    } catch (e) {
        document.getElementById('result').textContent = '解码或比较失败: ' + e;
    }
}
function fnShowAccuraySimilarityForAudio() {
    audio1Src=document.getElementById("sShowAudioFromAIGC").src;
    audio2Src=document.getElementById("sShowAudioFromKnowledgebase").src;
    alert(audio1Src+audio2Src);
    document.getElementById("sAccuracySimilarityForAudio").textContent=fnAudioSimilarity(audio1Src,audio2Src)
    }
**/
//const AudioContext = window.AudioContext || window.webkitAudioContext;
//let audioCtx;

async function fetchAndDecode(url) {
    const AudioContext = window.AudioContext || window.webkitAudioContext;
    let audioCtx;
  if (!audioCtx) audioCtx = new AudioContext();

  const response = await fetch(url);
  const arrayBuffer = await response.arrayBuffer();
  const audioBuffer = await audioCtx.decodeAudioData(arrayBuffer);

  // 取单声道并采样压缩（例如每秒取10个样本）
  const channelData = audioBuffer.getChannelData(0); // 单声道
  const sampleRate = audioBuffer.sampleRate;
  const samples = [];

  // 下采样：每秒取 10 个点
  const step = Math.floor(sampleRate / 10);
  for (let i = 0; i < channelData.length; i += step) {
    samples.push(channelData[i]);
  }

  return samples.slice(0, 100); // 截取前100个采样点用于比较
}

function cosineSimilarity(vecA, vecB) {
  const dot = vecA.reduce((sum, a, i) => sum + a * vecB[i], 0);
  const magA = Math.hypot(...vecA);
  const magB = Math.hypot(...vecB);
  return magA &&magB ? dot / (magA * magB) : 0;
}

async function fnCompareAudio() {
  const url1 = document.getElementById('sAudioFromAIGC').value;
  const url2 = document.getElementById('sAudioFromKnowledgebase').value;
  const resultDiv = document.getElementById('sAccuracySimilarityForAudio');

  if (!url1 || !url2) {
    resultDiv.innerHTML = "请填写两个有效的音频链接。";
    return;
  }

  try {
    resultDiv.innerHTML = "正在加载并处理音频...";

    const [samples1, samples2] = await Promise.all([
      fetchAndDecode(url1),
      fetchAndDecode(url2)
    ]);

    // 对齐长度
    const len = Math.min(samples1.length, samples2.length);
    const v1 = samples1.slice(0, len);
    const v2 = samples2.slice(0, len);

    // 计算余弦相似度
    const similarity = cosineSimilarity(v1, v2);
    const percentage = ((similarity + 1) / 2 * 100).toFixed(2); // 映射到 0~100%

    resultDiv.innerHTML = `
      <!--<p><strong>余弦相似度：</strong>${similarity.toFixed(4)}</p>-->
      <!--<span>余弦相似度：${similarity.toFixed(4)}</span>-->
      <!--<p><strong>相似度评分：</strong>${percentage}%</p>-->
       <span>${percentage/100}</span>
    `;
  } catch (error) {
    console.error(error);
    resultDiv.innerHTML += `<p><strong>错误：</strong>${error.message}</p>`;
  }
}