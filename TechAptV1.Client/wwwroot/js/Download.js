window.downloadStream = async (filename, contentStream,downloadType) => {
    const arrayBuffer = await contentStream.arrayBuffer();

    let blob;

    if (downloadType === "xml") {
         blob = new Blob([arrayBuffer], { type: "text/xml" });
    }
    else if (downloadType === "binary") {
         blob = new Blob([arrayBuffer], { type: "application/octet-stream" });
        }

    const link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
